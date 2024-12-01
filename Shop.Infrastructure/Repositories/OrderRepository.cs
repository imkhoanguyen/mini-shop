using Shop.Application.Repositories;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Enum;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Order?> GetAsync(Expression<Func<Order, bool>> expression, bool tracked = false)
        {
            var query = tracked ? _context.Orders.AsQueryable() : _context.Orders.AsNoTracking().AsQueryable();

            return await query.Include(o => o.OrderItems)
                .Include(o => o.ShippingMethod)
                .Include(o => o.Discount).FirstOrDefaultAsync(expression);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .Include(o => o.ShippingMethod) 
                .Include(o => o.Discount) 
                .OrderByDescending(o => o.Created) 
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            // Lấy danh sách các Order trong ngày với trạng thái PaymentReceived
            var orders = await _context.Orders
                .Where(o => o.Created >= startOfDay && o.Created <= endOfDay && o.Status == OrderStatus.PaymentReceived)
                .ToListAsync();

            // Tính tổng doanh thu bằng GetTotal()
            return orders.Sum(o => o.GetTotal());
        }


        public async Task<decimal> GetTotalRevenueByMonthAsync(int year, int month)
        {
            // Lấy khoảng thời gian đầu tháng và cuối tháng
            var startOfMonth = new DateTime(year, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

            // Lọc các Order theo tháng, năm và trạng thái PaymentReceived
            var orders = await _context.Orders
                .Where(o => o.Created >= startOfMonth && o.Created <= endOfMonth && o.Status == OrderStatus.PaymentReceived)
                .ToListAsync();

            // Tính tổng doanh thu
            return orders.Sum(o => o.GetTotal());
        }

        public async Task<decimal> GetTotalRevenueByYearAsync(int year)
        {
            // Lấy khoảng thời gian đầu năm và cuối năm
            var startOfYear = new DateTime(year, 1, 1);
            var endOfYear = startOfYear.AddYears(1).AddTicks(-1);

            // Lọc các Order theo năm và trạng thái PaymentReceived
            var orders = await _context.Orders
                .Where(o => o.Created >= startOfYear && o.Created <= endOfYear && o.Status == OrderStatus.PaymentReceived)
                .ToListAsync();

            // Tính tổng doanh thu
            return orders.Sum(o => o.GetTotal());
        }

        public async Task<int> CountOrdersTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Orders
                .Where(o => o.Created.Date == today && o.Status == OrderStatus.PaymentReceived)
                .CountAsync();
        }
        public async Task<int> CountOrdersByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            // Count Orders for the selected date with PaymentReceived status
            return await _context.Orders
                .Where(o => o.Created >= startOfDay && o.Created <= endOfDay && o.Status == OrderStatus.PaymentReceived)
                .CountAsync();
        }
        public async Task<int> CountOrdersByMonthAsync(int year, int month)
        {
            var startOfMonth = new DateTime(year, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

            return await _context.Orders
                .Where(o => o.Created >= startOfMonth && o.Created <= endOfMonth && o.Status == OrderStatus.PaymentReceived)
                .CountAsync();
        }

        public async Task<int> CountOrdersByYearAsync(int year)
        {
            var startOfYear = new DateTime(year, 1, 1);
            var endOfYear = startOfYear.AddYears(1).AddTicks(-1);

            return await _context.Orders
                .Where(o => o.Created >= startOfYear && o.Created <= endOfYear && o.Status == OrderStatus.PaymentReceived)
                .CountAsync();
        }

    }
}
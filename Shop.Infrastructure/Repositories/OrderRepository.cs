using Shop.Application.Repositories;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Enum;

namespace Shop.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .Include(o => o.ShippingMethod) 
                .Include(o => o.Discount) 
                .OrderByDescending(o => o.Order_date) 
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            // Lấy danh sách các Order trong ngày với trạng thái PaymentReceived
            var orders = await _context.Orders
                .Where(o => o.Order_date >= startOfDay && o.Order_date <= endOfDay && o.Status == OrderStatus.PaymentReceived)
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
                .Where(o => o.Order_date >= startOfMonth && o.Order_date <= endOfMonth && o.Status == OrderStatus.PaymentReceived)
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
                .Where(o => o.Order_date >= startOfYear && o.Order_date <= endOfYear && o.Status == OrderStatus.PaymentReceived)
                .ToListAsync();

            // Tính tổng doanh thu
            return orders.Sum(o => o.GetTotal());
        }

        public async Task<int> CountOrdersTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Orders
                .Where(o => o.Order_date.Date == today && o.Status == OrderStatus.PaymentReceived)
                .CountAsync();
        }

        public async Task<int> CountOrdersByMonthAsync(int year, int month)
        {
            var startOfMonth = new DateTime(year, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

            return await _context.Orders
                .Where(o => o.Order_date >= startOfMonth && o.Order_date <= endOfMonth && o.Status == OrderStatus.PaymentReceived)
                .CountAsync();
        }

        public async Task<int> CountOrdersByYearAsync(int year)
        {
            var startOfYear = new DateTime(year, 1, 1);
            var endOfYear = startOfYear.AddYears(1).AddTicks(-1);

            return await _context.Orders
                .Where(o => o.Order_date >= startOfYear && o.Order_date <= endOfYear && o.Status == OrderStatus.PaymentReceived)
                .CountAsync();
        }

    }
}
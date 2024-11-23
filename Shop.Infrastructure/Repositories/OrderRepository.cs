using Shop.Application.Repositories;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

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
    }
}
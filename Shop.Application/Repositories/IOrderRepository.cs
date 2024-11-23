using Shop.Domain.Entities;

namespace Shop.Application.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
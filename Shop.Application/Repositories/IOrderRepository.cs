using Shop.Domain.Entities;

namespace Shop.Application.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<decimal> GetTotalRevenueByDateAsync(DateTime date);
        Task<decimal> GetTotalRevenueByMonthAsync(int year, int month);
        Task<decimal> GetTotalRevenueByYearAsync(int year);
        Task<int> CountOrdersTodayAsync();
        Task<int> CountOrdersByDateAsync(DateTime date);
        Task<int> CountOrdersByMonthAsync(int year, int month);
        Task<int> CountOrdersByYearAsync(int year);
    }
}
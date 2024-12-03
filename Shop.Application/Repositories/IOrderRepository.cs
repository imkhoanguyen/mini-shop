using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<PagedList<Order>> GetAllAsync(OrderParams prm, bool tracked = false);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<decimal> GetTotalRevenueByDateAsync(DateTime date);
        Task<decimal> GetTotalRevenueByMonthAsync(int year, int month);
        Task<decimal> GetTotalRevenueByYearAsync(int year);
        Task<int> CountOrdersTodayAsync();
        Task<int> CountOrdersByDateAsync(DateTime date);
        Task<int> CountOrdersByMonthAsync(int year, int month);
        Task<int> CountOrdersByYearAsync(int year);
        Task<List<Order>> GetOrdersByYearAsync(int year);

        Task<(AppUser? User, decimal TotalAmount)> GetUserWithHighestTotalForTodayAsync();
        Task<(AppUser? User, decimal TotalAmount)> GetUserWithHighestTotalForDateAsync(DateTime? date);
        Task<(AppUser? User, decimal TotalAmount)> GetUserWithHighestTotalForMonthAsync(int month, int year);
        Task<(AppUser? User, decimal TotalAmount)> GetUserWithHighestTotalForYearAsync(int year);
    }
}
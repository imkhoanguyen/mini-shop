using Shop.Application.Repositories;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IProductUserLikeRepository : IRepository<ProductUserLike>
    {
        Task<List<Product>> GetFavoriteProductsByUserAsync(string userId);
        Task AddUserLikedProductAsync(string userId, int productId);
        Task<bool> ExistsAsync(string userId, int productId);
        Task RemoveAsync(ProductUserLike productUserLike);
        Task<ProductUserLike> GetUserLikedProductAsync(string userId, int productId);
    }
}
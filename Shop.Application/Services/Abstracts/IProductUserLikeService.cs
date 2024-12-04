using Shop.Application.DTOs.Products;


namespace Shop.Application.Services.Abstracts
{
    public interface IProductUserLikeService
    {
        Task<List<ProductDto>> GetFavoriteProductsByUserAsync(string userId);
        Task<bool> AddUserLikedProductAsync(string userId, int productId);
        Task<bool> RemoveUserLikedProductAsync(string userId, int productId);
        
    }
}

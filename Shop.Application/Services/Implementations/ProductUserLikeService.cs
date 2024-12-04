using Shop.Application.DTOs.Products;
using Shop.Application.Mappers;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;


namespace Shop.Application.Services.Implementations
{
    public class ProductUserLikeService : IProductUserLikeService
    {
        private readonly IUnitOfWork _unit;

        public ProductUserLikeService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<List<ProductDto>> GetFavoriteProductsByUserAsync(string userId)
        {
            var products = await _unit.ProductUserLikeRepository.GetFavoriteProductsByUserAsync(userId);
            return products.Select(ProductMapper.EntityToProductDto).ToList();
        }
        public async Task<bool> AddUserLikedProductAsync(string userId, int productId)
        {
            var alreadyLiked = await _unit.ProductUserLikeRepository.ExistsAsync(userId, productId);

            if (alreadyLiked)
                return false;

            await _unit.ProductUserLikeRepository.AddUserLikedProductAsync(userId, productId);
            return await _unit.CompleteAsync();
        }

        public async Task<bool> RemoveUserLikedProductAsync(string userId, int productId)
        {
            
            // Lấy sản phẩm yêu thích của user
            var userLikedProduct = await _unit.ProductUserLikeRepository.GetUserLikedProductAsync(userId, productId);

            if (userLikedProduct == null)
            {
                return false; // Nếu không tìm thấy, trả về false
            }

            // Xóa sản phẩm yêu thích
            await _unit.ProductUserLikeRepository.RemoveAsync(userLikedProduct);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _unit.CompleteAsync();
            return true;
        }
    }
}

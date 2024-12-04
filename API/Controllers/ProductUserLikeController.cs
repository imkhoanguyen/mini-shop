using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.AddLikedProduct;
using Shop.Application.DTOs.Products;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class ProductUserLikeController : BaseApiController
    {
        private readonly IProductUserLikeService _productUserLikeService;

        public ProductUserLikeController(IProductUserLikeService productUserLikeService)
        {
            _productUserLikeService = productUserLikeService;
        }

        [HttpGet("{userId}/favorites")]
        public async Task<ActionResult<List<ProductDto>>> GetFavoriteProducts(string userId)
        {
            var favoriteProducts = await _productUserLikeService.GetFavoriteProductsByUserAsync(userId);
             if (favoriteProducts == null || !favoriteProducts.Any())
                return NotFound(new { message = "No liked favoriteProducts found for this user." });

            return Ok(favoriteProducts);
        }       
        [HttpPost("liked-products")]
        public async Task<IActionResult> AddLikedProduct([FromBody] AddLikedProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productUserLikeService.AddUserLikedProductAsync(dto.UserId, dto.ProductId);

            if (!result)
                return Conflict(new { message = "Product is already liked by this user." });

            return Ok(new { message = "Product added to liked list successfully." });
        }
        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveUserLikedProductAsync(string userId,int productId)
        {
            var result = await _productUserLikeService.RemoveUserLikedProductAsync(userId, productId);

            if (!result)
            {
                return NotFound(new { message = "Sản phẩm yêu thích không tồn tại hoặc không thể xóa." });
            }

            return Ok(new { message = "Xóa sản phẩm yêu thích thành công." });
        }
    }
}

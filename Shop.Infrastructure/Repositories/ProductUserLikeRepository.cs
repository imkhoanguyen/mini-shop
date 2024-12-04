using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class ProductUserLikeRepository : Repository<ProductUserLike>, IProductUserLikeRepository
    {
        private readonly StoreContext _context;
        public ProductUserLikeRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetFavoriteProductsByUserAsync(string userId)
        {
            return await _context.ProductUserLikes
                .Where(pul => pul.AppUserId == userId)
                .Include(pul => pul.Product) .ThenInclude(pul=>pul.Variants)
                .Include(pul => pul.Product) .ThenInclude(pul=>pul.Variants) .ThenInclude(pul=>pul.Color)
                .Include(pul => pul.Product) .ThenInclude(pul=>pul.Variants) .ThenInclude(pul=>pul.Size)
                .Include(pul => pul.Product).ThenInclude(p => p.Image) 
                .Select(pul => pul.Product!)
                .ToListAsync();
        }
        public async Task AddUserLikedProductAsync(string userId, int productId)
        {
            var productUserLike = new ProductUserLike
            {
                AppUserId = userId,
                ProductId = productId
            };

            // Thêm vào database
            await _context.ProductUserLikes.AddAsync(productUserLike);
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _context.ProductUserLikes.AnyAsync(pul =>
                pul.AppUserId == userId && pul.ProductId == productId);
        }

        public async Task RemoveAsync(ProductUserLike productUserLike)
        {
            _context.ProductUserLikes.Remove(productUserLike);
            await _context.SaveChangesAsync(); // Xác nhận thay đổi ngay lập tức
        }
        public async Task<ProductUserLike> GetUserLikedProductAsync(string userId, int productId)
        {
            return await _context.ProductUserLikes
                .FirstOrDefaultAsync(pul => pul.AppUserId == userId && pul.ProductId == productId);
        }
    }
}
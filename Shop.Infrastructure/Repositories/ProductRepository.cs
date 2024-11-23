using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Ultilities;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var productDb = await _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .FirstOrDefaultAsync(c => c.Id == product.Id);
            if (productDb is null) return;
            
            productDb.Name = product.Name;
            productDb.Description = product.Description;
            productDb.Status = product.Status;
            productDb.Updated = DateTime.UtcNow.AddHours(7);
            if (product.Image != null)
            {
                productDb.Image = product.Image;
            }

            _context.ProductCategories.RemoveRange(productDb.ProductCategories);
            
            var newProductCategories = product.ProductCategories.Select(categoryId => new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = categoryId.CategoryId
            }).ToList();

            await _context.ProductCategories.AddRangeAsync(newProductCategories);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .Include(p => p.Variants.Where(v => !v.IsDelete))
                .ThenInclude(v => v.Images)
                .Where(c => !c.IsDelete).ToListAsync();
        }

        public override async Task<Product?> GetAsync(Expression<Func<Product, bool>> expression, bool tracked = false)
        {
            var query = _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .Include(p => p.Variants.Where(v => !v.IsDelete))
                .ThenInclude(v => v.Images)
                .Where(c => !c.IsDelete).AsQueryable();

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParams ProductParams, bool tracked = false)
        {
            var query = tracked ? _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .Include(p => p.Variants.Where(v => !v.IsDelete))
                .ThenInclude(v => v.Images)
                .Where(c => !c.IsDelete)
            : _context.Products.AsNoTracking().AsQueryable()
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .Include(p => p.Variants.Where(v => !v.IsDelete))
                .ThenInclude(v => v.Images)
                .Where(c => !c.IsDelete);

            if (!string.IsNullOrEmpty(ProductParams.Search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(ProductParams.Search.ToLower())
                    || c.Id.ToString() == ProductParams.Search);
            }

            if (!string.IsNullOrEmpty(ProductParams.OrderBy))
            {
                query = ProductParams.OrderBy switch
                {
                    "id" => query.OrderBy(c => c.Id),
                    "id_desc" => query.OrderByDescending(c => c.Id),
                    "name" => query.OrderBy(c => c.Name.ToLower()),
                    "name_desc" => query.OrderByDescending(c => c.Name.ToLower()),
                    _ => query.OrderByDescending(c => c.Id)
                };
            }

            return await query.ApplyPaginationAsync(ProductParams.PageNumber, ProductParams.PageSize);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool tracked = false)
        {
            if (tracked)
                return await _context.Products
                    .Include(p => p.ProductCategories)
                    .Include(p => p.Image)
                    .Include(p => p.Variants.Where(v => !v.IsDelete))
                    .ThenInclude(v => v.Images)
                    .Where(c => !c.IsDelete).ToListAsync();
            return await _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .Include(p => p.Variants) 
                .ThenInclude(v => v.Images)
                .Where(c => !c.IsDelete && c.Variants.All(v => !v.IsDelete)) 
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task DeleteProductAsync(Product product)
        {
            var productDb = await _context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (productDb is not null)
            {
                productDb.IsDelete = true;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(Expression<Func<Product, bool>> expression, bool tracked = false)
        {
            var query = tracked ? _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .Include(p => p.Variants.Where(v => !v.IsDelete))
                .ThenInclude(v => v.Images)
                .Where(c => !c.IsDelete)
            : _context.Products.AsNoTracking().AsQueryable()
                .Include(p => p.ProductCategories)
                .Include(p => p.Image)
                .Include(p => p.Variants.Where(v => !v.IsDelete))
                .ThenInclude(v => v.Images)
                .Where(c => !c.IsDelete);

            return await query.Where(expression).ToListAsync();
        }
    }
}
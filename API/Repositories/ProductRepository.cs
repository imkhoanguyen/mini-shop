using API.Data;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        private readonly IVariantRepository _variantRepository;
        public ProductRepository(StoreContext context, IVariantRepository variantRepository)
        {
            _context = context;
            _variantRepository = variantRepository;
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            //await _context.SaveChangesAsync();
        }
        public async Task AddProductCategory(Product product)
        {
            if (product.ProductCategories != null && product.ProductCategories.Count > 0)
            {
                foreach (var productCategory in product.ProductCategories)
                {
                    var existingProductCategory = await _context.ProductCategories
                        .FirstOrDefaultAsync(pc => pc.ProductId == product.Id && pc.CategoryId == productCategory.CategoryId);
                    if (existingProductCategory == null)
                    {
                        _context.ProductCategories.Add(new ProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = productCategory.CategoryId
                        });
                    }
                }
            }
        }
        public async Task UpdateProduct(Product product)
        {
            var productDb = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (productDb is not null)
            {
                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.Status = product.Status;
                productDb.Updated = DateTime.UtcNow;

                _context.ProductCategories.RemoveRange(productDb.ProductCategories);

                if (product.ProductCategories != null && product.ProductCategories.Count > 0)
                {
                    foreach (var productCategory in product.ProductCategories)
                    {
                        var newProductCategory = new ProductCategory
                        {
                            ProductId = productDb.Id,
                            CategoryId = productCategory.CategoryId
                        };
                        productDb.ProductCategories.Add(newProductCategory);
                    }
                }

            }

        }
        public void DeleteProduct(Product product)
        {
            var productDb = _context.Products
            .FirstOrDefault(p => p.Id == product.Id);
            if (productDb is not null)
            {
                productDb.IsDelete = true;

            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var productDb = await _context.Products
                .Include(p => p.Variants)
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDelete);


            return productDb;
        }

        public async Task<Product?> GetProductByName(string name)
        {
            var productDb = await _context.Products
                .Include(p => p.Variants)
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower() && !p.IsDelete);
            return productDb;
        }
        public async Task<bool> ProductExistsAsync(string name)
        {
            return await _context.Products.AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var productDb = await _context.Products
                .Include(p => p.Variants)
                .Include(p => p.ProductCategories)
                .Where(p => !p.IsDelete).ToListAsync();
            return productDb;
        }

        public async Task<PageList<Product>> GetAllProductsAsync(ProductParams productParams)
        {
            var query = _context.Products
                .Include(p => p.Variants)
                .Include(p => p.ProductCategories)
                .Where(p => !p.IsDelete)
                .OrderBy(p => p.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(productParams.SearchString))
            {
                query = query.Where(p => p.Name.ToLower().Contains(productParams.SearchString.ToLower())
                    || p.Id.ToString() == productParams.SearchString);
            }
            var count = await query.CountAsync();
            var productIds = await query.Skip((productParams.PageNumber - 1) * productParams.PageSize)
                                        .Take(productParams.PageSize)
                                        .Select(p => p.Id)
                                        .ToListAsync();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Include(p => p.Variants)
                .Include(p => p.ProductCategories)
                .ToListAsync();

            return new PageList<Product>(products, count, productParams.PageNumber, productParams.PageSize);
        }
    }
}
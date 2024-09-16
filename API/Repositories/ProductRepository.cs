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
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

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
            if (product.Variants != null && product.Variants.Count > 0)
            {
                foreach (var variant in product.Variants)
                {
                    variant.ProductId = product.Id;
                    _variantRepository.AddVariant(variant);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            var productDb = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == product.Id);
            if (productDb is not null)
            {
                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.Status = product.Status;
                productDb.Updated = DateTime.UtcNow;

                _context.ProductCategories.RemoveRange(productDb.ProductCategories);

                foreach (var productCategory in product.ProductCategories)
                {
                    var newProductCategory = new ProductCategory
                    {
                        ProductId = productDb.Id,
                        CategoryId = productCategory.CategoryId
                    };
                    productDb.ProductCategories.Add(newProductCategory);
                }
                _context.Products.Update(productDb);
                await _context.SaveChangesAsync();
            }

        }
        public void DeleteProduct(Product product)
        {
            var productDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productDb is not null)
            {
                productDb.IsDelete = true;
                _context.SaveChanges();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product?> GetProductByName(string name)
        {
            return await _context.Products.Where(p => !p.IsDelete && p.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }
        public async Task<bool> ProductExistsAsync(string name)
        {
            return await _context.Products.AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Where(p => !p.IsDelete).ToListAsync();
        }

        public async Task<PageList<Product>> GetAllProductsAsync(ProductParams productParams)
        {
            var query = _context.Products.Where(c => !c.IsDelete).OrderBy(c => c.Id).AsQueryable();
            if (!string.IsNullOrEmpty(productParams.SearchString))
            {
                query = query.Where(c => c.Name.ToLower().Contains(productParams.SearchString.ToLower())
                    || c.Id.ToString() == productParams.SearchString);
            }

            var count = await query.CountAsync();

            var items = await query.Skip((productParams.PageNumber - 1) * productParams.PageSize)
                                   .Take(productParams.PageSize)
                                   .ToListAsync();
            return new PageList<Product>(items, count, productParams.PageNumber, productParams.PageSize);

        }
    }
}
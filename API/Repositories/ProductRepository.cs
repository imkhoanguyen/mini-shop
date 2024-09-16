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
                .Include(p => p.Variants)
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
                _context.Variants.RemoveRange(productDb.Variants);

                if (product.Variants != null && product.Variants.Count > 0)
                {
                    foreach (var variant in product.Variants)
                    {
                        variant.ProductId = productDb.Id;
                        _context.Variants.Add(variant);
                    }
                }
                await _context.SaveChangesAsync();
            }

        }
        public void DeleteProduct(Product product)
        {
            var productDb = _context.Products
            .Include(p => p.Variants)
            .FirstOrDefault(p => p.Id == product.Id);
            if (productDb is not null)
            {
                productDb.IsDelete = true;
                if (product.Variants != null && product.Variants.Count > 0)
                {
                    foreach (var variant in product.Variants)
                    {
                        variant.ProductId = product.Id;
                        _variantRepository.DeleteVariant(variant);
                    }
                }
                _context.SaveChanges();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var productDb = await _context.Products
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDelete);
            var variantDb = await _variantRepository.GetVariantByProductIdAsync(id);
            productDb!.Variants = new List<Variant>();
            return productDb;
        }

        public async Task<Product?> GetProductByName(string name)
        {
            var productDb = await _context.Products
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower() && !p.IsDelete);
            var variantDb = await _variantRepository.GetVariantByProductIdAsync(productDb!.Id);
            productDb!.Variants = new List<Variant>();
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
                .Where(p => !p.IsDelete).ToListAsync();
            var productIds = productDb.Select(p => p.Id);
            var variants = await _variantRepository.GetAllByProductIdsAsync(productIds);

            foreach (var product in productDb)
            {
                product.Variants = variants.Where(v => v.ProductId == product.Id).ToList();
            }

            return productDb;
        }

        public async Task<PageList<Product>> GetAllProductsAsync(ProductParams productParams)
        {
            var query = _context.Products
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
                .ToListAsync();

            var variants = await _variantRepository.GetAllByProductIdsAsync(productIds);

            foreach (var product in products)
            {
                product.Variants = variants.Where(v => v.ProductId == product.Id).ToList();
            }

            return new PageList<Product>(products, count, productParams.PageNumber, productParams.PageSize);

        }
    }
}
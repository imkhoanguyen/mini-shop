using API.Helpers;
using API.Interfaces;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Product>> GetAllProductsAsync(ProductParams categoryParams)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
        //public async Task AddProductCategory(Product product)
        //{
        //    var existingCategories = await _context.ProductCategories
        //        .Where(pc => pc.ProductId == product.Id)
        //        .ToListAsync();

        //    if (existingCategories != null && existingCategories.Count > 0)
        //    {
        //        _context.ProductCategories.RemoveRange(existingCategories);
        //    }

        //    if (product.ProductCategories != null && product.ProductCategories.Count > 0)
        //    {
        //        foreach (var productCategory in product.ProductCategories)
        //        {
        //            _context.ProductCategories.Add(new ProductCategory
        //            {
        //                ProductId = product.Id,
        //                CategoryId = productCategory.CategoryId
        //            });
        //        }
        //    }
        //}

        //public async Task UpdateProduct(Product product)
        //{
        //    var productDb = await _context.Products
        //        .FirstOrDefaultAsync(p => p.Id == product.Id);

        //    if (productDb is not null)
        //    {
        //        productDb.Name = product.Name;
        //        productDb.Description = product.Description;
        //        productDb.Status = product.Status;
        //        productDb.Updated = DateTime.UtcNow;

        //        _context.ProductCategories.RemoveRange(productDb.ProductCategories);

        //        if (product.ProductCategories != null && product.ProductCategories.Count > 0)
        //        {
        //            foreach (var productCategory in product.ProductCategories)
        //            {
        //                var newProductCategory = new ProductCategory
        //                {
        //                    ProductId = productDb.Id,
        //                    CategoryId = productCategory.CategoryId
        //                };
        //                productDb.ProductCategories.Add(newProductCategory);
        //            }
        //        }

        //    }

        //}
        //public void DeleteProduct(Product product)
        //{
        //    var productDb = _context.Products
        //    .FirstOrDefault(p => p.Id == product.Id);
        //    if (productDb is not null)
        //    {
        //        productDb.IsDelete = true;

        //    }
        //}

        //public async Task<Product?> GetProductByIdAsync(int id)
        //{
        //    var productDb = await _context.Products
        //        .Include(p => p.Variants.Where(v => !v.IsDelete))
        //        .ThenInclude(v => v.Images)
        //        .Include(p => p.ProductCategories)
        //        .FirstOrDefaultAsync(p => p.Id == id && !p.IsDelete);


        //    return productDb;
        //}

        //public async Task<Product?> GetProductByName(string name)
        //{
        //    var productDb = await _context.Products
        //        .Include(p => p.Variants)
        //        .Include(p => p.ProductCategories)
        //        .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower() && !p.IsDelete);
        //    return productDb;
        //}


        //public async Task<PagedList<Product>> GetAllProductsAsync(ProductParams productParams)
        //{
        //    var query = _context.Products
        //        .Include(p => p.Variants.Where(v => !v.IsDelete))
        //        .ThenInclude(v => v.Images)
        //        .Include(p => p.ProductCategories)
        //        .Where(p => !p.IsDelete)
        //        .OrderBy(p => p.Id)
        //        .AsQueryable();
        //    if (!string.IsNullOrEmpty(productParams.SearchString))
        //    {
        //        query = query.Where(p => p.Name.ToLower().Contains(productParams.SearchString.ToLower())
        //            || p.Id.ToString() == productParams.SearchString);
        //    }
        //    var count = await query.CountAsync();
        //    var productIds = await query.Skip((productParams.PageNumber - 1) * productParams.PageSize)
        //                                .Take(productParams.PageSize)
        //                                .Select(p => p.Id)
        //                                .ToListAsync();
        //    var products = await _context.Products
        //        .Where(p => productIds.Contains(p.Id))
        //        .Include(p => p.Variants.Where(v => !v.IsDelete))
        //        .ThenInclude(v => v.Images)
        //        .Include(p => p.ProductCategories)
        //        .ToListAsync();

        //    return new PageList<Product>(products, count, productParams.PageNumber, productParams.PageSize);
        //}
    }
}
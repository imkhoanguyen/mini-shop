using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task AddProduct(Product product)
        {
            // Bước 1: Thêm sản phẩm và lưu nó vào cơ sở dữ liệu trước
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
                await _context.SaveChangesAsync();
            }
        }


        public async Task UpdateProduct(Product product)
        {
            var productDb = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == product.Id);
            if (productDb is not null)
            {
                // Cập nhật thông tin của sản phẩm
                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.Status = product.Status;
                productDb.Updated = DateTime.UtcNow;

                // Xóa các productcategory cũ
                _context.ProductCategories.RemoveRange(productDb.ProductCategories);

                // Thêm các productcategory mới (nếu có)
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


    }
}
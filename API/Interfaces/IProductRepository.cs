using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        void DeleteProduct(Product product);

        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductByName(string name);
        Task<bool> ProductExistsAsync(string name);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<PageList<Product>> GetAllProductsAsync(ProductParams categoryParams);


    }
}
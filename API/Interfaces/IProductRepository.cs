using API.Entities;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductByName(string name);

    }
}
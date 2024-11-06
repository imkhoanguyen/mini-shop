
using API.Helpers;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<PagedList<Product>> GetAllProductsAsync(ProductParams categoryParams);

    }
}
using API.Helpers;
using Shop.Application.DTOs.Auth.Facebook;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace API.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetAllProductsAsync(ProductParams ProductParams, bool tracked = false);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool tracked = false);
        Task<IEnumerable<Product>> GetProductsAsync(Expression<Func<Product, bool>> expression, bool tracked = false);
        Task DeleteProductAsync(Product Product);

        Task UpdateProductAsync(Product Product);

    }
}
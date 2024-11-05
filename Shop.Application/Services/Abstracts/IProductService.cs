using API.Helpers;
using Shop.Application.DTOs.Products;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Application.Services.Abstracts
{
    public interface IProductService
    {
        Task<ProductDto> AddAsync(ProductAdd productAdd);
        Task<ProductDto> UpdateAsync(ProductUpdate productUpdate);
        Task DeleteAsync(Expression<Func<Product, bool>> expression);
        Task<PagedList<ProductDto>> GetAllAsync(ProductParams productParams, bool tracked);
        Task<IEnumerable<ProductDto>> GetAllAsync(bool tracked);
        Task<ProductDto?> GetAsync(Expression<Func<Product, bool>> expression);
    }
}

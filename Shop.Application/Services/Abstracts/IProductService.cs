using API.Helpers;
using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Products;
using Shop.Application.DTOs.Reviews;
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
        Task<ProductDto> AddImageAsync(int productId, IFormFile file);
        Task RemoveImageAsync(int productId, int imageId);
        Task<PagedList<ProductDto>> GetAllAsync(ProductParams productParams, bool tracked);
        Task<IEnumerable<ProductDto>> GetAllAsync(bool tracked);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int categoryId);
        Task<ProductDto?> GetAsync(Expression<Func<Product, bool>> expression);
    }
}

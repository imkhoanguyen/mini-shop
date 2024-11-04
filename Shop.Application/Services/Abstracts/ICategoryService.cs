using Shop.Application.DTOs.Categories;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Application.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<CategoryDto> AddAsync(CategoryAdd categoryAdd);
        Task<CategoryDto> UpdateAsync(CategoryUpdate categoryUpdate);
        Task DeleteAsync(Expression<Func<Category,bool>> expression);
        Task<PagedList<CategoryDto>> GetAllAsync(CategoryParams categoryParams, bool tracked);
        Task<IEnumerable<CategoryDto>> GetAllAsync(bool tracked);
        Task<CategoryDto?> GetAsync(Expression<Func<Category, bool>> expression);

    }
}

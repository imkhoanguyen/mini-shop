using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams, bool tracked = false);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool tracked = false);
        Task DeleteCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);
    }
}
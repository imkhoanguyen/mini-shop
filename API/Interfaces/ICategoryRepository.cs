using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        Task<Category?> GetCategoriesById(int id);
        Task<string?> GetCategoryNameById(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<bool> CategoryExistsAsync(string name);
        Task<PageList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext _context;
        public CategoryRepository(StoreContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

        }
        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            var categoryDb = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if(categoryDb is not null){
                categoryDb.IsDelete = true;
                _context.SaveChanges();
            }
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

         public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Where(c => !c.IsDelete).ToListAsync();

        }
      
    }
}
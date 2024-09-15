using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Helper;
using API.Helpers;
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
            var categoryDb = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (categoryDb is not null)
            {
                categoryDb.Name = category.Name;
                categoryDb.Updated = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }
        public void DeleteCategory(Category category)
        {
            var categoryDb = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (categoryDb is not null)
            {
                categoryDb.IsDelete = true;
                _context.SaveChanges();
            }
        }

        public async Task<Category?> GetCategoriesById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Where(c => !c.IsDelete).ToListAsync();

        }

        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<PageList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams)
        {
            var query = _context.Categories.Where(c => !c.IsDelete).OrderBy(c => c.Id).AsQueryable();
            if (!string.IsNullOrEmpty(categoryParams.SearchString))
            {
                query = query.Where(c => c.Name.ToLower().Contains(categoryParams.SearchString.ToLower())
                    || c.Id.ToString() == categoryParams.SearchString);
            }

            var count = await query.CountAsync();

            var items = await query.Skip((categoryParams.PageNumber - 1) * categoryParams.PageSize)
                                   .Take(categoryParams.PageSize)
                                   .ToListAsync();
            return new PageList<Category>(items, count, categoryParams.PageNumber, categoryParams.PageSize);
        }



    }
}
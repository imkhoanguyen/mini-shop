using Microsoft.EntityFrameworkCore;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Ultilities;

namespace Shop.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly StoreContext _context;
        public CategoryRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var categoryDb = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryDb is not null)
            {
                categoryDb.Name = category.Name;
                categoryDb.Updated = DateTime.UtcNow;
            }
        }


        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Where(c => !c.IsDelete).ToListAsync();
        }


        public async Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams, bool tracked = false)
        {
            var query = tracked ? _context.Categories.AsQueryable().Where(c => !c.IsDelete) 
                : _context.Categories.AsNoTracking().AsQueryable().Where(c => !c.IsDelete);

            if (!string.IsNullOrEmpty(categoryParams.Search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(categoryParams.Search.ToLower())
                    || c.Id.ToString() == categoryParams.Search);
            }

            if (!string.IsNullOrEmpty(categoryParams.OrderBy))
            {
                query = categoryParams.OrderBy switch
                {
                    "id" => query.OrderBy(c => c.Id),
                    "id_desc" => query.OrderByDescending(c => c.Id),
                    "name" => query.OrderBy(c => c.Name.ToLower()),
                    "name_desc" => query.OrderByDescending(c => c.Name.ToLower()),
                    _ => query.OrderByDescending(c => c.Id)
                };
            }

            return await query.ApplyPaginationAsync(categoryParams.PageNumber,categoryParams.PageSize);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool tracked = false)
        {
            if (tracked)
                return await _context.Categories.Where(c => !c.IsDelete).ToListAsync();
            return await _context.Categories.AsNoTracking().Where(c => !c.IsDelete).ToListAsync();
        }

        public  async Task DeleteCategoryAsync(Category category)
        {
            var categoryDb = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryDb is not null)
            {
                categoryDb.IsDelete = true;
            }
        }
    }
}
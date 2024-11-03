using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Ultilities;

namespace Shop.Infrastructure.Repositories
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        private readonly StoreContext _context;

        public ColorRepository(StoreContext context) : base(context)
        {
            _context = context;
        }


        public async Task<PagedList<Color>> GetAllColorsAsync(ColorParams colorParams, bool tracked)
        {
            var query = tracked ? _context.Colors.AsQueryable().Where(c => !c.IsDelete)
                : _context.Colors.AsNoTracking().AsQueryable().Where(c => !c.IsDelete);

            if (!string.IsNullOrEmpty(colorParams.Search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(colorParams.Search.ToLower())
                    || c.Id.ToString() == colorParams.Search);
            }

            if (!string.IsNullOrEmpty(colorParams.OrderBy))
            {
                query = colorParams.OrderBy switch
                {
                    "id" => query.OrderBy(c => c.Id),
                    "id_desc" => query.OrderByDescending(c => c.Id),
                    "name" => query.OrderBy(c => c.Name.ToLower()),
                    "name_desc" => query.OrderByDescending(c => c.Name.ToLower()),
                    _ => query.OrderByDescending(c => c.Id)
                };
            }

            return await query.ApplyPaginationAsync(colorParams.PageNumber, colorParams.PageSize);

        }

        public async Task<IEnumerable<Color>> GetAllColorsAsync(bool tracked = false)
        {
            if (tracked)
                return await _context.Colors.Where(c => !c.IsDelete).ToListAsync();
            return await _context.Colors.AsNoTracking().Where(c => !c.IsDelete).ToListAsync();
        }

        public async Task DeleteAsync(Color color)
        {
            var colorDb = await _context.Colors.FirstOrDefaultAsync(c => c.Id == color.Id);
            if (colorDb is not null)
            {
                colorDb.IsDelete = true;
            }
        }

        public async Task UpdateAsync(Color color)
        {
            var colorDb = await _context.Colors.FirstOrDefaultAsync(c => c.Id == color.Id);
            if (colorDb is not null)
            {
                colorDb.Name = color.Name;
            }
        }
    }
}
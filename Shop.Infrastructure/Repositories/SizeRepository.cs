using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Ultilities;

namespace API.Repositories
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        private readonly StoreContext _context;

        public SizeRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Size size)
        {
            var sizeDb = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == size.Id);
            if (sizeDb is not null)
            {
                sizeDb.IsDelete = true;
            }
        }


        public async Task<IEnumerable<Size>> GetAllSizesAsync(bool tracked)
        {
            if (tracked)
                return await _context.Sizes.Where(c => !c.IsDelete).ToListAsync();
            return await _context.Sizes.AsNoTracking().Where(c => !c.IsDelete).ToListAsync();
        }

        public Task<PagedList<Size>> GetAllSizesAsync(SizeParams sizeParams, bool tracked = false)
        {
            var query = tracked ? _context.Sizes.AsQueryable().Where(c => !c.IsDelete)
               : _context.Sizes.AsNoTracking().AsQueryable().Where(c => !c.IsDelete);

            if (!string.IsNullOrEmpty(sizeParams.SearchString))
            {
                query = query.Where(c => c.Name.ToLower().Contains(sizeParams.SearchString.ToLower())
                    || c.Id.ToString() == sizeParams.SearchString);
            }

            if (!string.IsNullOrEmpty(sizeParams.OrderBy))
            {
                query = sizeParams.OrderBy switch
                {
                    "id" => query.OrderBy(s => s.Id),
                    "id_desc" => query.OrderByDescending(s => s.Id),
                    "name" => query.OrderBy(s => s.Name.ToLower()),
                    "name_desc" => query.OrderByDescending(s => s.Name.ToLower()),
                    _ => query.OrderByDescending(s => s.Id)
                };
            }

            return query.ApplyPaginationAsync(sizeParams.PageNumber, sizeParams.PageSize);
        }


        public async Task UpdateAsync(Size size)
        {
            var sizeDb = await _context.Sizes.FirstOrDefaultAsync(c => c.Id == size.Id);
            if (sizeDb is not null)
            {
                sizeDb.Name = size.Name;
            }
        }

        public async Task<IEnumerable<Size>> GetAllSizesAsync()
        {
            return await _context.Sizes.Where(c => !c.IsDelete).ToListAsync();
        }

        public async Task<Size?> GetSizesById(int id)
        {
            return await _context.Sizes.FindAsync(id);
        }

        public async Task<bool> SizeExistsAsync(string name)
        {
            return await _context.Sizes.AnyAsync(c => c.Name == name);
        }

        public async Task AddSize(Size size)
        {
            await _context.Sizes.AddAsync(size);
        }

    }
}
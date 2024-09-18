using API.Data;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly StoreContext _context;

        public SizeRepository(StoreContext context)
        {
            _context = context;
        }
        public void AddSize(Size size)
        {
            _context.Sizes.Add(size);
        }

        public void DeleteSize(Size size)
        {
            var sizeDb = _context.Sizes.FirstOrDefault(c => c.Id == size.Id);
            if (sizeDb is not null)
            {
                sizeDb.IsDelete = true;
            }
        }

        public async Task<PageList<Size>> GetAllSizesAsync(SizeParams sizeParams)
        {
            var query = _context.Sizes.Where(c => !c.IsDelete).OrderBy(c => c.Id).AsQueryable();
            if (!string.IsNullOrEmpty(sizeParams.SearchString))
            {
                query = query.Where(c => c.Name.ToLower().Contains(sizeParams.SearchString.ToLower())
                    || c.Id.ToString() == sizeParams.SearchString);
            }

            var count = await query.CountAsync();

            var items = await query.Skip((sizeParams.PageNumber - 1) * sizeParams.PageSize)
                                   .Take(sizeParams.PageSize)
                                   .ToListAsync();
            return new PageList<Size>(items, count, sizeParams.PageNumber, sizeParams.PageSize);
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

        public void UpdateSize(Size size)
        {
            var sizeDb = _context.Sizes.FirstOrDefault(c => c.Id == size.Id);
            if (sizeDb is not null)
            {
                sizeDb.Name = size.Name;
            }
        }

    
    }
}
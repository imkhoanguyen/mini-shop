using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Helper;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly StoreContext _context;

        public ColorRepository(StoreContext context)
        {
            _context = context;
        }
        public void AddColor(Color color)
        {
            _context.Colors.Add(color);
            _context.SaveChanges();
        }

        public void DeleteColor(Color color)
        {
            var colorDb = _context.Colors.FirstOrDefault(c => c.Id == color.Id);
            if (colorDb is not null)
            {
                colorDb.IsDelete = true;

                _context.SaveChanges();
            }
        }

        public async Task<PageList<Color>> GetAllColorsAsync(ColorParams colorParams)
        {
            var query = _context.Colors.Where(c => !c.IsDelete).OrderBy(c => c.Id).AsQueryable();
            if (!string.IsNullOrEmpty(colorParams.SearchString))
            {
                query = query.Where(c => c.Name.ToLower().Contains(colorParams.SearchString.ToLower())
                    || c.Id.ToString() == colorParams.SearchString);
            }

            var count = await query.CountAsync();

            var items = await query.Skip((colorParams.PageNumber - 1) * colorParams.PageSize)
                                   .Take(colorParams.PageSize)
                                   .ToListAsync();
            return new PageList<Color>(items, count, colorParams.PageNumber, colorParams.PageSize);
        }

        public async Task<Color?> GetColorsById(int id)
        {
            return await _context.Colors.FindAsync(id);
        }

        public async Task<bool> colorExistsAsync(string name)
        {
            return await _context.Colors.AnyAsync(c => c.Name == name);
        }

        public void UpdateColor(Color color)
        {
            var colorDb = _context.Colors.FirstOrDefault(c => c.Id == color.Id);
            if (colorDb is not null)
            {
                colorDb.Name = color.Name;

                _context.SaveChanges();
            }
        }

        async Task<IEnumerable<Color>> IColorRepository.GetAllColorsAsync()
        {
            return await _context.Colors.Where(c => !c.IsDelete).ToListAsync();
        }
    }
}
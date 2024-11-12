using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.Application.Repositories;
using Shop.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly StoreContext _context;
        public BlogRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Blog blog)
        {
            var blogDb = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blog.Id);
            if (blogDb is not null)
            {
                _context.Remove(blogDb);
            }
        }

        public async Task<IEnumerable<Blog>> GetAllAsync(bool tracked)
        {
            if (tracked)
                return await _context.Blogs.ToListAsync();
            return await _context.Blogs.AsNoTracking().ToListAsync();
        }

        public async Task<Blog?> getById(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

    }
}
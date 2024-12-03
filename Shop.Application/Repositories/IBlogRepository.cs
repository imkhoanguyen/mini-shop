using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Application.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task DeleteAsync(Blog blog);
        Task<IEnumerable<Blog>> GetAllAsync(bool tracked);
        Task<Blog?> getById(int id);
        Task UpdateBlogAsync(Blog blog);
    }
}
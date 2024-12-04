using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync(bool tracked);
        Task<IEnumerable<Blog>> GetAll5BlogsAsync(bool tracked);
        Task<PagedList<Blog>> GetAllBlogsAsync(BlogParams blogParams, bool tracked = false);
        Task<Blog?> GetBlogsById(int id);
        Task DeleteAsync(Blog blog);
        Task UpdateBlogAsync(Blog blog);

        Task AddAsync(Blog blog);

    }
}
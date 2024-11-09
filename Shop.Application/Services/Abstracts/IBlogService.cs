using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using Shop.Application.DTOs.Blogs;
using Shop.Application.DTOs.Categories;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
namespace Shop.Application.Services.Abstracts
{
    public  interface IBlogService
    {
        Task<BlogDto> AddAsync(BlogsAdd blogAdd);
        Task<BlogDto> UpdateAsync(BlogUpdate blogUpdate);
        Task DeleteAsync(Expression<Func<Blog, bool>> expression);
        Task<PagedList<BlogDto>> GetAllAsync(BlogParams blogParams, bool tracked);
        Task<IEnumerable<BlogDto>> GetAllAsync(bool tracked);
        Task<BlogDto?> GetAsync(Expression<Func<Blog, bool>> expression);
    }
}

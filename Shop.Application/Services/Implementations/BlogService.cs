using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException(nameof(blog), "blog not null");
            }
            await _unitOfWork.BlogRepository.AddAsync(blog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
            await _unitOfWork.BlogRepository.DeleteAsync(blog);
        }

        public async Task<IEnumerable<Blog>> GetAll5BlogsAsync(bool tracked)
        {
            var blogs = await _unitOfWork.BlogRepository.GetAllAsync(tracked);
            return blogs.OrderByDescending(b => b.Create).Take(5);
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync(bool tracked)
        {
            var blogs = await _unitOfWork.BlogRepository.GetAllAsync(tracked);
            return blogs.OrderByDescending(b => b.Create);
        }

        public Task<PagedList<Blog>> GetAllBlogsAsync(BlogParams blogParams, bool tracked = false)
        {
            throw new NotImplementedException();
        }

        public async Task<Blog?> GetBlogsById(int id)
        {
            return await _unitOfWork.BlogRepository.getById(id);
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            await _unitOfWork.BlogRepository.UpdateBlogAsync(blog);
        }
    }
}
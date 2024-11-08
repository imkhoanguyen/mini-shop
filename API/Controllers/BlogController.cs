using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Blog;
using Shop.Application.Mappers;
using Shop.Application.Parameters;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBlogsAsync()
        {
            var blogs = await _blogService.GetAllBlogsAsync(false);

            var blogsDto = blogs.Select(b => BlogMapper.EntityToBlogDto(b)).ToList();
            return Ok(blogsDto);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetBlogsById(int id)
        {
            var blog = await _blogService.GetBlogsById(id);
            var blogDto = BlogMapper.EntityToBlogDto(blog);
            return Ok(blogDto);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> AddBlog(BlogAddDto blogAddDto)
        {
            var blog = BlogMapper.BlogAddDtoToEntity(blogAddDto);
            await _blogService.AddAsync(blog);

            return Ok(new { message = "Thêm blog thanh cong" });
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateBlog(int id, BlogAddDto blogAddDto)
        {
            var blog = await _blogService.GetBlogsById(id);
            var blogUpdate = BlogMapper.BlogAddDtoToEntity(blogAddDto);
            blog.Create = blogUpdate.Create;
            blog.Update = blogUpdate.Update;
            blog.Content = blogUpdate.Content;
            blog.Title = blogUpdate.Title;
            await _blogService.UpdateAsync(blog);

            return Ok(new { message = "Sua blog thanh cong" });
        }
    }
}
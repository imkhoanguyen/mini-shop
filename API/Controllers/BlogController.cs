using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Blog;
using Shop.Application.Mappers;
using Shop.Application.Parameters;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Exceptions;

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

        [HttpGet("List5Blog")]
        public async Task<IActionResult> GetAll5BlogsAsync()
        {
            var blogs = await _blogService.GetAll5BlogsAsync(false);

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
            blog.Create = blogAddDto.Create;
            blog.Update = blogAddDto.Update;
            blog.Content = blogAddDto.Content;
            blog.Title = blogAddDto.Title;
            await _blogService.UpdateBlogAsync(blog);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _blogService.UpdateBlogAsync(blog);
            return Ok(blog);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteBlog(int id)
        {
            var blog = await _blogService.GetBlogsById(id);
            if (blog == null) throw new NotFoundException("Không tìm thấy blog");

            await _blogService.DeleteAsync(blog);

            return Ok(new { message = "xoa thanh cong" });
        }
    }
}
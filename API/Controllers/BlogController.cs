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
        public async Task<IActionResult> GetAllblogsAsync()
        {
            var blogs = await _blogService.GetAllBlogsAsync(false);

            var blogsDto = blogs.Select(b => BlogMapper.EntityToBlogDto(b)).ToList();
            return Ok(blogsDto);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetblogsById(int id)
        {
            var blog = await _blogService.GetBlogsById(id);
            var blogDto = BlogMapper.EntityToBlogDto(blog);
            return Ok(blogDto);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Addblog(BlogAddDto blogAddDto)
        {
            var blog = BlogMapper.BlogAddDtoToEntity(blogAddDto);
            await _blogService.AddAsync(blog);

            return Ok(new { message = "Thêm blog thanh cong" });
        }
    }
}
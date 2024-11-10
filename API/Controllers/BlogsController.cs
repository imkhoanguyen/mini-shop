using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using API.Controllers;
using System.Reflection.Emit;
using Shop.Application.Services.Abstracts;
using API.Extensions;
using Shop.Application.DTOs.Categories;
using Shop.Application.Parameters;
using Shop.Application.Services.Implementations;
using Shop.Application.DTOs.Blogs;

namespace API.Controllers
{
    
    public class BlogsController : BaseApiController
    {
        private readonly IBlogService _blogService;
        public BlogsController(IBlogService BlogService)
        {
            _blogService = BlogService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<BlogDto>>> GetAllBlogs([FromQuery] BlogParams BlogsParams)
        //{
        //    var pagedList = await _blogService.GetAllAsync(BlogsParams, false);
        //    Response.AddPaginationHeader(pagedList);
        //    return Ok(pagedList);
        //}

        //[HttpGet("all")]
        //public async Task<ActionResult<IEnumerable<BlogDto>>> GetAllBlogs()
        //{
        //    var blogs = await _blogService.GetAllAsync(false);
        //    return Ok(blogs);
        //}
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<BlogDto>> GetBlogsById(int id)
        //{
        //    var blog = await _blogService.GetAsync(c => c.Id == id);
        //    return Ok(blog);
        //}

        //[HttpPost("Add")]
        //public async Task<IActionResult> AddBlogs(BlogsAdd blogsAdd)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var blog = await _blogService.AddAsync(blogsAdd);
        //    return CreatedAtAction(nameof(GetAllBlogs), new { id = blog.Id }, blog);
        //}

        //[HttpPut("Update")]
        //public async Task<IActionResult> UpdateBlogs(BlogUpdate blogDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var blog = await _blogService.UpdateAsync(blogDto);
        //    return Ok(blog);
        //}

        //[HttpDelete("Delete")]
        //public async Task<IActionResult> DeleteBlogs(int id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    await _blogService.DeleteAsync(c => c.Id == id);
        //    return NoContent();
        //}

    }
}

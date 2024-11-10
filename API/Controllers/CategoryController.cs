using API.Controllers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Categories;
using Shop.Application.Parameters;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;


namespace api.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories([FromQuery] CategoryParams categoryParams)
        {
            var pagedList = await _categoryService.GetAllAsync(categoryParams, false);

            Response.AddPaginationHeader(pagedList);
            return Ok(pagedList);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync(false);
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetAsync(c => c.Id == id);
            return Ok(category);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory( CategoryAdd categoryAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.AddAsync(categoryAdd);
            return CreatedAtAction(nameof(GetAllCategories), new { id = category.Id }, category);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory( CategoryUpdate categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _categoryService.UpdateAsync(categoryDto);
            return Ok(category);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _categoryService.DeleteAsync(c => c.Id == id);
            return NoContent();
        }
    }
}

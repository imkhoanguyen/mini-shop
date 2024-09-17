using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/category/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            if(categories == null)
            {
                return NotFound("Không tìm thấy danh mục nào.");
            }
            var categoryDto = categories.Select(c => Category.toCategoryDto(c)).ToList();
            return Ok(categoryDto);
        }
        // GET api/category/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesById(id);
            if(categories == null)
            {
                return NotFound("Không tìm thấy danh mục nào.");
            }
            var categoryDto = Category.toCategoryDto(categories);
            return Ok(categoryDto);
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync([FromQuery] CategoryParams categoryParams)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync(categoryParams);
            if(categories == null)
            {
                return NotFound("Không tìm thấy danh mục nào.");
            }
            var categoriesDto = categories.Select(c => Category.toCategoryDto(c)).ToList();
            return Ok(categoriesDto);
        }

        // POST api/category/Add
        [HttpPost("Add")]
        public async Task<ActionResult> AddCategory(CategoryAddDto categoryAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.CategoryRepository.CategoryExistsAsync(categoryAddDto.Name!))
            {
                return BadRequest("Danh mục với tên này đã tồn tại.");
            }
            var category = CategoryAddDto.toCategory(categoryAddDto);
            _unitOfWork.CategoryRepository.AddCategory(category);
 
            if (await _unitOfWork.Complete())
                return NoContent();
            return Ok("Add Category successfully.");
        }

        // PUT api/category/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.CategoryRepository.CategoryExistsAsync(categoryDto.Name!))
            {
                return BadRequest("Danh mục với tên này đã tồn tại.");
            }
            var category = CategoryDto.toCategory(categoryDto);
            _unitOfWork.CategoryRepository.UpdateCategory(category);

            if (await _unitOfWork.Complete()){
                var updatedCategoryDto = Category.toCategoryDto(category);
                return Ok(updatedCategoryDto);
            }

            return Ok("Update Category successfully.");
        }

        // DELETE api/category/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = CategoryDto.toCategory(categoryDto);
            _unitOfWork.CategoryRepository.DeleteCategory(category);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Delete Category successfully.");
        }
    }
}

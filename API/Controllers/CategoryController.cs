using api.Interfaces;
using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{


    public class CategoryController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET api/category/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }
        // GET api/category/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesById(id);
            return Ok(categories);
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync([FromQuery] CategoryParams categoryParams)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync(categoryParams);
            return Ok(categories);
        }

        // POST api/category/Add
        [HttpPost("Add")]
        public async Task<ActionResult> AddCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.CategoryRepository.CategoryExistsAsync(categoryDto.Name!))
            {
                return BadRequest("Danh mục với tên này đã tồn tại.");
            }
            var category = _mapper.Map<Category>(categoryDto);  // Map Dto to Entity
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
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWork.CategoryRepository.UpdateCategory(category);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Update Category successfully.");
        }

        // DELETE api/category/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWork.CategoryRepository.DeleteCategory(category);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Delete Category successfully.");
        }
    }
}

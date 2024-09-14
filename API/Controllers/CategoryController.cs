using api.Interfaces;
using API.Controllers;
using API.DTOs;
using API.Entities;
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

        // POST api/category/Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = _mapper.Map<Category>(categoryDto); 
            _unitOfWork.CategoryRepository.AddCategory(category);
            var result = await _unitOfWork.Complete();

            if (result) 
                return Ok("Category added successfully");


            return BadRequest("An error occurred while adding the category.");
        }

        // PUT api/category/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWork.CategoryRepository.UpdateCategory(category);

            if (await _unitOfWork.Complete())
                return NoContent();

            return BadRequest("An error occurred while updating the category.");
        }

        // DELETE api/category/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = _mapper.Map<Category>(categoryDto);  
            _unitOfWork.CategoryRepository.DeleteCategory(category);

            if (await _unitOfWork.Complete())
                return NoContent();

            return BadRequest("An error occurred while deleting the category.");
        }
    }
}

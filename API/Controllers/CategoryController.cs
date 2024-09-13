using api.Interfaces;
using API.Controllers;
using API.Entities;
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
            return Ok(categories);
        }

        // POST api/category/Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.CategoryRepository.AddCategory(category);

            return BadRequest("An error occurred while adding the category.");
        }

        // PUT api/category/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.CategoryRepository.UpdateCategory(category);

            if (await _unitOfWork.Complete())
                return NoContent();

            return BadRequest("An error occurred while updating the category.");
        }

        // DELETE api/category/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.CategoryRepository.DeleteCategory(category);

            if (await _unitOfWork.Complete())
                return NoContent();

            return BadRequest("An error occurred while deleting the category.");
        }
    }
}

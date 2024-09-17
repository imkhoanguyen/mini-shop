using api.Interfaces;
using API.Controllers;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    public class ProductController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/Product/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var categories = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            return Ok(categories);
        }

        // POST api/Product/Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] Product Product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.ProductRepository.AddProduct(Product);

            return BadRequest("An error occurred while adding the Product.");
        }

        // PUT api/Product/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product Product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.ProductRepository.UpdateProduct(Product);

            if (await _unitOfWork.Complete())
                return NoContent();

            return BadRequest("An error occurred while updating the Product.");
        }

        // DELETE api/Product/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct([FromBody] Product Product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.ProductRepository.DeleteProduct(Product);

            if (await _unitOfWork.Complete())
                return NoContent();

            return BadRequest("An error occurred while deleting the Product.");
        }
        
    }
}

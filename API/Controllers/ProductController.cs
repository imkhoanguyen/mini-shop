using api.Interfaces;
using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _unitOfWork.ProductRepository.ProductExistsAsync(productDto.Name!))
            {
                return BadRequest("Sản phẩm với tên này đã tồn tại.");
            }

            var product = _mapper.Map<Product>(productDto);

            // Đảm bảo rằng Id của product đã được gán
            if (product.Id != 0)
            {
                return BadRequest("Product Id đã được gán trước khi thêm vào cơ sở dữ liệu.");
            }

            await _unitOfWork.ProductRepository.AddProduct(product);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Thêm sản phẩm thành công.");
        }


        // PUT api/Product/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.ProductRepository.ProductExistsAsync(productDto.Name!))
            {
                return BadRequest("Sản phẩm với tên này đã tồn tại.");
            }
            var product = _mapper.Map<Product>(productDto);

            await _unitOfWork.ProductRepository.UpdateProduct(product);
            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Update Product successfully.");

        }

        // DELETE api/Product/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = _mapper.Map<Product>(productDto);
            _unitOfWork.ProductRepository.DeleteProduct(product);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Delete Product successfully.");
        }
    }
}

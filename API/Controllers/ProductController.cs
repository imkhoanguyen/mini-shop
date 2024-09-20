using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public ProductController(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        [HttpGet("GetProductById{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            if (product == null) return NotFound("Không tìm thấy sản phẩm nào.");
            var productDto = Product.toProductDto(product);
            return Ok(productDto);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var product = await _unitOfWork.ProductRepository.GetAllProductsAsync();
            if (product == null)
            {
                return NotFound("Không tìm thấy sản phẩm nào.");
            }
            var productDto = product.Select(p => Product.toProductDto(p)).ToList();
            return Ok(productDto);
        }
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByName(name);
            if (product == null) return NotFound("không tìm thấy sản phẩm với tên = " + name);
            var productDto = Product.toProductDto(product);
            return Ok(productDto);
        }
        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllProductsAsync([FromQuery] ProductParams productParams)
        {
            var product = await _unitOfWork.ProductRepository.GetAllProductsAsync(productParams);
            if (product == null)
            {
                return NotFound("Không tìm thấy sản phẩm nào.");
            }
            var productDto = product.Select(p => Product.toProductDto(p)).ToList();
            return Ok(productDto);
        }

        // POST api/Product/Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromForm] ProductAddDto productAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _unitOfWork.ProductRepository.ProductExistsAsync(productAddDto.Name!))
            {
                return BadRequest("Sản phẩm với tên này đã tồn tại.");
            }
            var product = await ProductAddDto.toProduct(productAddDto, _imageService);

            if (product.Id != 0)
            {
                return BadRequest("Product Id đã được gán trước khi thêm vào cơ sở dữ liệu.");
            }
            await _unitOfWork.ProductRepository.AddProduct(product);

            if (await _unitOfWork.Complete())
                return Ok("Add Product successfully.");
            return BadRequest("Add Product failed.");
        }

        // PUT api/Product/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.ProductRepository.ProductExistsAsync(productDto.Name!))
            {
                return BadRequest("Sản phẩm với tên này đã tồn tại.");
            }
            var product = await ProductDto.toProduct(productDto, _imageService);

            await _unitOfWork.ProductRepository.UpdateProduct(product);
            if (await _unitOfWork.Complete())
                return Ok("Update Product successfully.");
            return BadRequest("Update Product failed.");
        }

        // DELETE api/Product/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await ProductDto.toProduct(productDto, _imageService);
            _unitOfWork.ProductRepository.DeleteProduct(product);

            if (await _unitOfWork.Complete())
                return Ok("Delete Product successfully.");
            return BadRequest("Delete Product failed.");
            
        }
    }
}

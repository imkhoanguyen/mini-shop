using API.Controllers;
using API.Extensions;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Products;
using Shop.Application.Services.Abstracts;
using Shop.Application.Services.Implementations;
using Shop.Application.Ultilities;

namespace api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery] ProductParams productParams)
        {
            var pagedList = await _productService.GetAllAsync(productParams, false);

            Response.AddPaginationHeader(pagedList);
            return Ok(pagedList);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var categories = await _productService.GetAllAsync(false);
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetAsync(c => c.Id == id);
            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromForm] ProductAdd productAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.AddAsync(productAdd);
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdate productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _productService.UpdateAsync(productDto);
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _productService.DeleteAsync(c => c.Id == id);
            return NoContent();
        }
        [HttpPost("add-images/{productId:int}")]
        public async Task<IActionResult> AddImagesProduct([FromRoute] int productId, [FromForm] IFormFile imageFile)
        {
            var product = await _productService.AddImageAsync(productId, imageFile);
            return Ok(product);
        }

        [HttpDelete("remove-image/{productId:int}")]
        public async Task<IActionResult> RemoveImagesProduct([FromRoute] int productId, int imageId)
        {
            await _productService.RemoveImageAsync(productId, imageId);
            var product = await _productService.GetAsync(r => r.Id == productId);
            return NoContent();
        }

    }
}

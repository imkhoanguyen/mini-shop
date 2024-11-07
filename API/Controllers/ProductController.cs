using API.Controllers;
using API.Extensions;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Products;
using Shop.Application.Services.Abstracts;
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
        public async Task<ActionResult<PagedList<ProductDto>>> GetAllCategories([FromQuery] ProductParams productParams)
        {
            var pagedList = await _productService.GetAllAsync(productParams, false);

            Response.AddPaginationHeader(pagedList);
            return Ok(pagedList);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllCategories()
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
        public async Task<IActionResult> AddProduct(ProductAdd productAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.AddAsync(productAdd);
            return CreatedAtAction(nameof(GetAllCategories), new { id = product.Id }, product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct(ProductUpdate productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _productService.UpdateAsync(productDto);
            return Ok(product);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _productService.DeleteAsync(c => c.Id == id);
            return NoContent();
        }

    }
}

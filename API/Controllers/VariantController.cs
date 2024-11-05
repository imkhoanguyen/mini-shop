using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Variants;
using Shop.Application.Mappers;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;

namespace API.Controllers
{
    public class VariantController : BaseApiController
    {
        private readonly IVariantService _variantService;
        private readonly IProductService _productService;
        private readonly ICloudinaryService _cloudinaryService;

        public VariantController(IVariantService variantService, ICloudinaryService cloudinaryService, IProductService productService)
        {
            _variantService = variantService;
            _cloudinaryService = cloudinaryService;
            _productService = productService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VariantDto>> GetVariantById(int id)
        {
            var variant = await _variantService.GetAsync(c => c.Id == id);
            return Ok(variant);
        }
        [HttpGet("GetByProductId")]
        public async Task<ActionResult<IEnumerable<VariantDto>>> GetByProductId(int productId)
        {
            var variants = await _variantService.GetByProductId(productId);
            return Ok(variants);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddVariant([FromForm] VariantAdd variantAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _productService.GetAsync(c => c.Id == variantAdd.ProductId);
            if (product == null) return NotFound("Sản phẩm không tồn tại");

            await _variantService.AddAsync(variantAdd);
            return Ok(variantAdd);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateVariant(VariantUpdate variantDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var variant = await _variantService.UpdateAsync(variantDto);
            return Ok(variant);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteVariant(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _variantService.DeleteAsync(c => c.Id == id);
            return NoContent();
        }
    }
}
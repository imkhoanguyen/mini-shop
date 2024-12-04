using API.Controllers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Abstracts;
using Shop.Application.Parameters;
using Shop.Application.DTOs.Discounts;
using CloudinaryDotNet.Actions;
using Shop.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Shop.Application.Ultilities;

namespace API.Controllers
{
    public class DiscountController : BaseApiController
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountDto>>> GetAllDiscount([FromQuery] DiscountParams parameters)
        {
            var discounts = await _discountService.GetAllAsync(parameters, false);
            Response.AddPaginationHeader(discounts);
            return Ok(discounts);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DiscountDto>> GetDiscountById(int id)
        {
            var discount = await _discountService.GetAsync(dc => dc.Id == id);
            return Ok(discount);
        }
        [HttpPost("Add")]
        [Authorize(Policy = ClaimStore.Discount_Create)]
        public async Task<ActionResult> AddDiscount(DiscountAdd discountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var dc = await _discountService.AddAsync(discountDto);
            return CreatedAtAction(nameof(GetAllDiscount), new { id = dc.Id }, dc);
        }
        [HttpPut("Update")]
        [Authorize(Policy = ClaimStore.Discount_Edit)]
        public async Task<ActionResult> UpdateDiscount(DiscountUpdate discountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var discount = await _discountService.UpdateAsync(discountDto);
            return Ok(discount);
        }
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<DiscountDto>>> GetAllDiscount()
        {
            var discounts = await _discountService.GetAllDiscount(false);
            return Ok(discounts);
        }
        [HttpDelete("Delete")]
        [Authorize(Policy = ClaimStore.Discount_Delete)]
        public async Task<ActionResult> DeleteDiscount(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _discountService.DeleteAsync(dc => dc.Id == id);
            return NoContent();
        }

        [HttpGet("code")]
        public async Task<ActionResult<DiscountDto>> GetDiscountBycode([FromQuery] string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new BadRequestException("Code cannot be null or empty.");
            }

            var discount = await _discountService.GetAsync(d => d.PromotionCode == code);

            if (discount == null)
            {
                throw new NotFoundException("Discount not found.");
            }

            return Ok(discount);
        }

    }

}
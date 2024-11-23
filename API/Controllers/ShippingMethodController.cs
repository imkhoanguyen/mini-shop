using API.Controllers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Abstracts;
using Shop.Application.Parameters;
using Shop.Application.DTOs.ShippingMethods;
using CloudinaryDotNet.Actions;

namespace API.Controllers
{
    public class ShippingMethodController : BaseApiController
    {
        private readonly IShippingMethodService _shippingMethodService;
        public ShippingMethodController(IShippingMethodService shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }
        // [HttpGet("GetAll")]
        // public async Task<IActionResult> GetAllShippingMethodAsync()
        // {
        //     var sms = await _shippingMethodService.GetShippingMethodsAsync(false);

        //     var smDtos = sms.Select(sm => ShippingMethodMapper.ShippingMethodEntityToDto(sm)).ToList();
        //     return Ok(smDtos);
        // }
         [HttpGet]
       public async Task<ActionResult<IEnumerable<ShippingMethodDto>>> GetAllShippingMethod([FromQuery]ShippingMethodParameters parameters){
            var shippingMethods = await _shippingMethodService.GetAllAsync(parameters, false);
            Response.AddPaginationHeader(shippingMethods);
            return Ok(shippingMethods);
       }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShippingMethodDto>> GetShippingMethodById (int id)
        {
            var method= await _shippingMethodService.GetAsync(sm=>sm.Id==id);
            return Ok(method);
        }
        [HttpPost("Add")]
        public async Task<ActionResult> AddShippingMethod(ShippingMethodAdd shippingMethodDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var sm = await _shippingMethodService.AddAsync(shippingMethodDto);
            return CreatedAtAction(nameof(GetAllShippingMethod),new {id=sm.Id},sm);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> UpdateShippingMethod(ShippingMethodUpdate shippingMethodDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var sm = await _shippingMethodService.UpdateAsync(shippingMethodDto);
            return Ok(sm);
        }
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<ShippingMethodDto>>>GetAllShippingMethod(){
            var shippingMethods=await _shippingMethodService.GetAllShippingMethod(false);
            return Ok(shippingMethods);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteShippingMethod(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            await _shippingMethodService.DeleteAsync(sm=>sm.Id==id);
            return NoContent();
        }
       
    }
    
}
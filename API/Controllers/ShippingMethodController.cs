using Microsoft.AspNetCore.Mvc;
using Shop.Application.Mappers;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class ShippingMethodController : BaseApiController
    {
        private readonly IShippingMethodService _shippingMethodService;
        public ShippingMethodController(IShippingMethodService shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllShippingMethodAsync()
        {
            var sms = await _shippingMethodService.GetShippingMethodsAsync(false);

            var smDtos = sms.Select(sm => ShippingMethodMapper.ShippingMethodEntityToDto(sm)).ToList();
            return Ok(smDtos);
        }
    }
}
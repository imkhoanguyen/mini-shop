using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Orders;
using Shop.Application.Interfaces;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSessionCheckout([FromBody]OrderAddDto dto)
        {
            dto.UserId = ClaimsPrincipleExtensions.GetUserId(User);
            var paymentUrl = await _paymentService.CreateCheckoutSessionAsync(dto);
            return Ok(new { Url = paymentUrl });
        }
    }
}

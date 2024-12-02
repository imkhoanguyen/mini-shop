using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.DTOs.Orders;
using Shop.Application.Interfaces;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Exceptions;
using Stripe;
using Stripe.Checkout;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unit;
        private readonly ICartService _cartService;
        private readonly string _whSecret;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger, IConfiguration config, IUnitOfWork unit, ICartService cartService)
        {
            _paymentService = paymentService;
            _logger = logger;
            _config = config;
            _unit = unit;
            _cartService = cartService;
            _whSecret = config["StripeSettings:WhSecret"]!;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSessionCheckout([FromBody] CreateSessionCheckoutDto dto)
        {
            if(dto.CartId.IsNullOrEmpty())
            {
                throw new BadRequestException("Không tìm thấy giỏ hàng");
            }
            dto.Order.UserId = ClaimsPrincipleExtensions.GetUserId(User);
            var paymentUrl = await _paymentService.CreateCheckoutSessionAsync(dto.Order, dto.CartId);
            return Ok(new { Url = paymentUrl });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = ConstructStripeEvent(json);

                switch (stripeEvent.Type)
                {
                    case "checkout.session.completed":
                        var session = stripeEvent.Data.Object as Session;

                        if (session != null)
                        {
                            var metadata = session.Metadata;
                            var orderId = metadata.ContainsKey("order_id") ? metadata["order_id"] : null;
                            var cartId = metadata.ContainsKey("cart_id") ? metadata["cart_id"] : null;

                            if (orderId != null)
                            {
                                await HandlePaymentSucceeded(orderId); // update status 
                            }
                            else
                            {
                                _logger.LogError("Checkout session metadata does not contain 'order_id'.");
                            }

                            // remove cart if checkout success
                            if(cartId != null)
                            {
                                await _cartService.DeleteCartAsync(cartId);
                            } else {
                                _logger.LogError("Checkout session metadata does not contain 'cart_id'.");
                            }
                        }
                        break;

                    default:
                        _logger.LogInformation($"Unhandled event type: {stripeEvent.Type}");
                        break;
                }

                return Ok(); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Stripe webhook.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        private async Task HandlePaymentSucceeded(string orderId)
        {
            if (int.TryParse(orderId, out int parsedOrderId))
            {
                var order = await _unit.OrderRepository.GetAsync(o => o.Id == parsedOrderId, true);
                order.Status = Shop.Domain.Enum.OrderStatus.PaymentReceived;
                await _unit.CompleteAsync();
            }
            else
            {
                _logger.LogError($"Invalid orderId: {orderId}");
            }
        }


        private Event ConstructStripeEvent(string json)
        {
            try
            {
                return EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                    _whSecret);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to construct stripe event");
                throw new StripeException("Invalid signature");
            }
        }
    }
}

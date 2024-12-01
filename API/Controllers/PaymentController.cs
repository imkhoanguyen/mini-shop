using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Orders;
using Shop.Application.Interfaces;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Stripe;
using Stripe.Checkout;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _config;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unit;
        private readonly string _whSecret;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger, IConfiguration config, IUnitOfWork unit)
        {
            _paymentService = paymentService;
            _logger = logger;
            _config = config;
            _unit = unit;
            _whSecret = config["StripeSettings:WhSecret"]!;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSessionCheckout([FromBody] OrderAddDto dto)
        {
            dto.UserId = ClaimsPrincipleExtensions.GetUserId(User);
            var paymentUrl = await _paymentService.CreateCheckoutSessionAsync(dto);
            return Ok(new { Url = paymentUrl });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                // Phân tích dữ liệu JSON từ webhook và tạo Event
                var stripeEvent = ConstructStripeEvent(json);

                // Kiểm tra loại sự kiện
                switch (stripeEvent.Type)
                {
                    // Khi thanh toán hoàn thành
                    case "checkout.session.completed":
                        var session = stripeEvent.Data.Object as Session;

                        if (session != null)
                        {
                            // Truy xuất metadata từ session
                            var metadata = session.Metadata;
                            var orderId = metadata.ContainsKey("order_id") ? metadata["order_id"] : null;

                            if (orderId != null)
                            {
                                await HandlePaymentSucceeded(orderId);
                            }
                            else
                            {
                                _logger.LogError("Checkout session metadata does not contain 'order_id'.");
                            }
                        }
                        break;

                    default:
                        _logger.LogInformation($"Unhandled event type: {stripeEvent.Type}");
                        break;
                }

                return Ok(); // Trả về OK khi xử lý xong sự kiện
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có vấn đề trong quá trình xử lý
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
                // Xử lý nếu không thể chuyển đổi orderId thành int (ví dụ: log lỗi)
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

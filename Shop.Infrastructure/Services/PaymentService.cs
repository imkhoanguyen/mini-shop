using Microsoft.Extensions.Configuration;
using Shop.Application.DTOs.Orders;
using Shop.Application.Interfaces;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Exceptions;
using Stripe;
using Stripe.Checkout;

namespace Shop.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unit;
        private readonly IOrderService _orderService;
        private readonly string _successUrl;
        private readonly string _cancelUrl;

        public PaymentService(IConfiguration config, ICartService cartService, IUnitOfWork unit, IOrderService orderService)
        {
            _cartService = cartService;
            _unit = unit;
            _orderService = orderService;
            StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
            _successUrl = config["StripeSettings:SuccessUrl"]!;
            _cancelUrl = config["StripeSettings:CancelUrl"]!;
        }

        public async Task<string> CreateCheckoutSessionAsync(OrderAddDto dto, string cartId)
        {

            var order = await _orderService.AddAsync(dto);
            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = $"{_successUrl}?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{_cancelUrl}?order_id={order.Id}",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    Metadata = new Dictionary<string, string>
                    {
                        {"order_id", order.Id.ToString() },
                        {"cart_id", cartId }
                    },
                };

                // Add items to the session
                foreach (var item in dto.Items)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"{item.ProductName} - {item.ColorName} - {item.SizeName} x {item.Quantity}",
                                Images = new List<string> { item.ProductImage }
                            }
                        },
                        Quantity = item.Quantity,
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                // Add shipping fee to the session
                var shippingLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(dto.ShippingFee * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Phí vận chuyển",
                        }
                    },
                    Quantity = 1,
                };
                options.LineItems.Add(shippingLineItem);

                // Add coupon if have
                if (dto.DiscountPrice.HasValue && dto.DiscountPrice > 0)
                {
                    // Create a coupon (one-time use)
                    var couponOptions = new CouponCreateOptions
                    {
                        AmountOff = (long)(dto.DiscountPrice.Value * 100),
                        Currency = "usd",
                        Name = "Giảm giá toàn bộ đơn hàng"
                    };

                    var couponService = new CouponService();
                    var coupon = await couponService.CreateAsync(couponOptions);

                    // Attach discount to the session
                    options.Discounts = new List<SessionDiscountOptions>
                    {
                        new SessionDiscountOptions
                        {
                            Coupon = coupon.Id
                        }
                    };
                }

                // Create Stripe session
                var service = new SessionService();
                var session = await service.CreateAsync(options);

                //update stripe sessionId to order
                var orderToUpdate = await _unit.OrderRepository.GetAsync(o => o.Id == order.Id, true);
                orderToUpdate.StripeSessionId = session.Id;
                await _unit.CompleteAsync();
                return session.Url;
            }
            catch
            {
                var orderToRemove = await _unit.OrderRepository.GetAsync(o => o.Id == order.Id);
                _unit.OrderRepository.Remove(orderToRemove);
                await _unit.CompleteAsync();
            }
            throw new BadRequestException("Problem when create checkout session");
        }
    }
}

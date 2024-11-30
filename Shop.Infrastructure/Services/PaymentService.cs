using Microsoft.Extensions.Configuration;
using Shop.Application.DTOs.Orders;
using Shop.Application.Interfaces;
using Shop.Application.Repositories;
using Stripe;

namespace Shop.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unit;

        public PaymentService(IConfiguration config, ICartService cartService, IUnitOfWork unit)
        {
            _cartService = cartService;
            _unit = unit;
            StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
        }

        public Task<string> CreateCheckoutSessionAsync(OrderAddDto dto)
        {
            throw new NotImplementedException();
        }

        public Task HandleWebhookAsync(string json, string stripeSignature)
        {
            throw new NotImplementedException();
        }
    }
}

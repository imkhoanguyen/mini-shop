using Shop.Application.DTOs.Orders;

namespace Shop.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(OrderAddDto dto);
        Task HandleWebhookAsync(string json, string stripeSignature);
    }
}

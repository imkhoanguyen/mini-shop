using Shop.Domain.Enum;

namespace Shop.Application.DTOs.Orders
{
    public class OrderDto
    {
       
        // address & user info
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? UserId { get; set; }

        // shipping method
        public decimal ShippingFee { get; set; }
        public decimal ShippingMethodId { get; set; }
        public required string ShippingName { get; set; }
        public required string ShippingMethodDesciption { get; set; }

        // discount 
        public int? DiscountId { get; set; }
        public decimal? DiscountPrice { get; set; }

        // order
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public List<OrderItemsDto> OrderItems { get; set; } = new List<OrderItemsDto>();

        // payment method
        public string? PaymentMethod { get; set; }
    }
}
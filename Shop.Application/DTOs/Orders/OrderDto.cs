using System.Numerics;

namespace Shop.Application.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal ShippingMethodId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public required string UserId { get; set; }
        public int DiscountId { get; set; }
        public decimal DiscountPrice { get; set; }
        public List<OrderItemsDto> orderItems { get; set; } = new List<OrderItemsDto>();
    }
}
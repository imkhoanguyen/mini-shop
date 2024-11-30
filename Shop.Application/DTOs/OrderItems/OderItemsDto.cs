using System.Numerics;

namespace Shop.Application.DTOs.Orders
{
    public class OrderItemsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public required string ProductName { get; set; }
        public required string SizeName { get; set; }
        public required string ColorName { get; set; }
        public string? ProductImage { get; set; }

        public int OrderId { get; set; }
    }
}
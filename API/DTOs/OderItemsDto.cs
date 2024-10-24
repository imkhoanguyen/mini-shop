using System.Numerics;

namespace API.DTOs
{
    public class OrderItemsDto
    {
        public int? order_id { get; set; }
        public int? product_id { get; set; }
        public int? quantity { get; set; }
        public decimal? price { get; set; }
        public decimal? subtotal { get; set; }
        public string? product_name { get; set; }
        public string? size_name { get; set; }
        public string? color_name { get; set; }
    }
}
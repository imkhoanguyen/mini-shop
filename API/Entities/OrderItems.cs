using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using API.Data.Enum;
using API.DTOs;

namespace API.Entities
{
    public class OrderItems : BaseEntity
    {
       
        public int? order_id { get; set; }
        [ForeignKey("order_id")]
        public Order? Order { get; set; }
        public int? product_id { get; set; }
        [ForeignKey("product_id")]
        public Product? Product { get; set; }
        public int? quantity { get; set; }
        public decimal? price { get; set; }
        public decimal? subtotal { get; set; }
        public string? product_name { get; set; }
        public string? size_name { get; set; }
        public string? color_name { get; set; }
        
        public static OrderItemsDto toOrderItemsDto(OrderItems OrderItems)
        {
            return new OrderItemsDto
            {   
                order_id=OrderItems.order_id,
                product_id=OrderItems.product_id,
                quantity=OrderItems.quantity,
                price=OrderItems.price,
                subtotal=OrderItems.subtotal,
                product_name=OrderItems.product_name,
                size_name=OrderItems.size_name,
                color_name=OrderItems.color_name,

            };
        }
    }
}
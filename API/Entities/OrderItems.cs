using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class OrderItems : BaseEntity
    {
        public int OrderId{get;set;}
        public int ProductId { get; set; }
        public int Quantity{get;set;}
        public decimal Price{get;set;}
        public decimal Subtotal { get; set; }
        public string ProductName{get;set;}
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order{get;set;}
        [ForeignKey("ProductId")]
        public Product? Product{get;set;}
        public int ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod? ShippingMethod { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class OrderItems : BaseEntity
    {
        public int orderId{get;set;}
        public int productId { get; set; }
        public int quantity{get;set;}
        public decimal price{get;set;}
        public decimal subtotal { get; set; }
        public string productName{get;set;}
        public string sizeName { get; set; }
        public string colorName { get; set; }
        [ForeignKey("orderId")]
        public Order? order{get;set;}
        [ForeignKey("productId")]
        public Product? product{get;set;}
    }
}
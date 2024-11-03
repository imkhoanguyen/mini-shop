using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class OrderItems : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public required string ProductName { get; set; }
        public required string SizeName { get; set; }
        public required string ColorName { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public decimal GetTotal()
        {
            return Price * Quantity;
        }
    }
}
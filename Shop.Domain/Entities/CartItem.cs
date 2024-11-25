using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    [NotMapped]
    public class CartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public required string ProductName { get; set; }
        public required string SizeName { get; set; }
        public required string ColorName { get; set; }
        public required string ProductImage { get; set; }
        public int ProductId { get; set; }
    }
}

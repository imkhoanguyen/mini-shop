using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Variant : BaseEntity
    {
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public int SizeId { get; set; }
        
        [ForeignKey("SizeId")]
        public Size? Size { get; set; }
        
        public int ColorId { get; set; }
        
        [ForeignKey("ColorId")]
        public Color? Color { get; set; }
        public int Status { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
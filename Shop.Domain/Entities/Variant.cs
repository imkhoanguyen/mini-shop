using Shop.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Variant : BaseEntity
    {
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        public VariantStatus Status { get; set; } = VariantStatus.Public;
        public bool IsDelete { get; set; } = false;

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public int? SizeId { get; set; }
        public Size? Size { get; set; }

        public int? ColorId { get; set; }
        public Color? Color { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();

        
    }
}

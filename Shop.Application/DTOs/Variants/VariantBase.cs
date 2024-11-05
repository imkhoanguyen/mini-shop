using Shop.Domain.Enum;

namespace Shop.Application.DTOs.Variants
{
    public class VariantBase
    {
        public required int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public VariantStatus Status { get; set; }
       
    }
}

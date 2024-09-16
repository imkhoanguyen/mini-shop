namespace API.DTOs
{
    public class VariantDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        // public List<SizeDto> Sizes { get; set; } = new List<SizeDto>();
        // public List<ColorDto> Colors { get; set; } = new List<ColorDto>();
    }
}
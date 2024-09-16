using API.Entities;

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
        public static Variant toVariant(VariantDto variantDto)
        {
            return new Variant
            {
                Id = variantDto.Id,
                Price = variantDto.Price,
                PriceSell = variantDto.PriceSell,
                Quantity = variantDto.Quantity,
                // Sizes = variantDto.Sizes.Select(s => Size.toSizeDto(s)).ToList(),
                // Colors = variantDto.Colors.Select(c => Color.toColorDto(c)).ToList()
            };
        }
    }
    public class VariantAddDto
    {
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        // public List<SizeDto> Sizes { get; set; } = new List<SizeDto>();
        // public List<ColorDto> Colors { get; set; } = new List<ColorDto>();
        public static Variant toVariant(VariantAddDto variantAddDto)
        {
            return new Variant
            {
                Price = variantAddDto.Price,
                PriceSell = variantAddDto.PriceSell,
                Quantity = variantAddDto.Quantity,
                // Sizes = variantDto.Sizes.Select(s => Size.toSizeDto(s)).ToList(),
                // Colors = variantDto.Colors.Select(c => Color.toColorDto(c)).ToList()
            };
        }
    }
}
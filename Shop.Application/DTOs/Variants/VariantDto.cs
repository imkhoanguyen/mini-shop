
using Shop.Application.DTOs.Colors;
using Shop.Application.DTOs.Size;

namespace Shop.Application.DTOs.Variants
{
    public class VariantDto : VariantBase
    {
        public int Id { get; set; }
        public SizeDto? Size { get; set; }
        public ColorDto? Color { get; set; }
        public List<ImageVariantDto> Images { get; set; } = new List<ImageVariantDto>();
    }
}

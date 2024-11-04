
namespace Shop.Application.DTOs.Variants
{
    public class VariantDto : VariantBase
    {
        public int Id { get; set; }
        public List<ImageVariantDto> Images { get; set; } = new List<ImageVariantDto>();
    }
}

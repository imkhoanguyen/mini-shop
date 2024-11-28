
namespace Shop.Application.DTOs.Variants
{
    public class VariantUpdate : VariantBase
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
    }
}

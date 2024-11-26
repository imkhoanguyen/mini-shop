using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Variants
{
    public class VariantAdd : VariantBase
    {
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
    }
}

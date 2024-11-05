using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Variants
{
    public class VariantAdd : VariantBase
    {
        public List<IFormFile> ImageFile { get; set; } = new List<IFormFile>();
    }
}

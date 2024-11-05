using Microsoft.AspNetCore.Http;
using Shop.Domain.Enum;

namespace Shop.Application.DTOs.Variants
{
    public class VariantUpdate : VariantBase
    {
        public int Id { get; set; }
        public List<IFormFile> ImageFile { get; set; } = new List<IFormFile>();
    }
}

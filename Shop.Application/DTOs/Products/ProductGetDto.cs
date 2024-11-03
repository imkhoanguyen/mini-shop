using API.DTOs;
using Shop.Application.DTOs.Variants;

namespace Shop.Application.DTOs.Products
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<VariantGetDto> Variants { get; set; } = new List<VariantGetDto>();
        public int Status { get; set; }
    }
}

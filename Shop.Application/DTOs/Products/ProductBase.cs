using Shop.Domain.Enum;

namespace Shop.Application.DTOs.Products
{
    public class ProductBase
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ProductStatus Status { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}

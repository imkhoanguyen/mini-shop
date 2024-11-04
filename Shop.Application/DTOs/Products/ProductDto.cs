
using Shop.Application.DTOs.Variants;
using Shop.Domain.Enum;

namespace Shop.Application.DTOs.Products
{
    public class ProductDto : ProductBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<VariantDto> Variants { get; set; } = new List<VariantDto>();

    }
}
using System.ComponentModel.DataAnnotations.Schema;
using API.Data.Enum;
using API.DTOs;

namespace API.Entities
{
    public class Variant : BaseEntity
    {
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        public VariantStatus Status { get; set; } = VariantStatus.Draft;
        public bool IsDelete { get; set; } = false;
        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public int? SizeId { get; set; }
        public Size? Size { get; set; }
        
        public int? ColorId { get; set; }
        public Color? Color { get; set; }
        
        public List<Image> Images { get; set; } = new List<Image>();

        public static VariantDto ToVariantDto(Variant variant)
        {
            return new VariantDto
            {
                Id = variant.Id,
                Price = variant.Price,
                PriceSell = variant.PriceSell,
                Quantity = variant.Quantity,
                SizeId = variant.SizeId,
                ColorId = variant.ColorId,
                ImageUrls = variant.Images.Select(i => new ImageGetDto
                {
                    Id = i.Id,
                    Url = i.Url,
                    IsMain = i.IsMain
                }).ToList()
            };
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Data.Enum;
using API.DTOs;
using Microsoft.VisualBasic;

namespace API.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Draft;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public bool IsDelete { get; set; } = false;
        public ICollection<Variant> Variants { get; set; } = new List<Variant>();

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<OrderItems> OrderItems{get;set;}=new List<OrderItems>();


        public static ProductGetDto toProductGetDto(Product product)
        {
            return new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Created = product.Created,
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                Variants = product.Variants.Select(v => new VariantDto
                {
                    Id = v.Id,
                    Price = v.Price,
                    PriceSell = v.PriceSell,
                    Quantity = v.Quantity,
                    SizeId = v.SizeId,
                    ColorId = v.ColorId
                }).ToList(),
                ImageUrls = product.Images.Select(i => new ImageGetDto 
                {
                    Id = i.Id, 
                    Url = i.Url, 
                    IsMain = i.IsMain 
                }).ToList()
            };
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Data.Enum;
using API.DTOs;
using API.Helpers;
using Microsoft.VisualBasic;

namespace API.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public CartItems CartItems{get;set;}
        public ProductStatus Status { get; set; } = ProductStatus.Draft;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public bool IsDelete { get; set; } = false;
        public ICollection<Variant> Variants { get; set; } = new List<Variant>();
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public List<Review> Reviews { get; set; } = new List<Review>();


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
                    SizeId = v.SizeId ?? 0,
                    ColorId = v.ColorId ?? 0,
                    ImageUrls = v.Images.Select(i => new ImageGetDto 
                    {
                        Id = i.Id, 
                        Url = i.Url, 
                        IsMain = i.IsMain 
                    }).ToList()
                }).ToList(),
                
            };
        }

        public static PageListDto<ProductGetDto> toPageListDto(PageList<Product> pageList)
        {
            return new PageListDto<ProductGetDto>
            {
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize,
                TotalCount = pageList.TotalCount,
                TotalPages = pageList.TotalPages,
                Items = pageList.Select(c => toProductGetDto(c)).ToList() 
            };
        }
    }
}
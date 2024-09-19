using System.ComponentModel.DataAnnotations;
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
        public DateTime Updated { get; set; }
        public bool IsDelete { get; set; } = false;
        public ICollection<Variant> Variants { get; set; } = new List<Variant>();

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Review> Reviews { get; set; } = new List<Review>();


        public static ProductDto toProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                VariantId = product.Variants.FirstOrDefault()?.Id ?? 0,
                Price = product.Variants.FirstOrDefault()?.Price ?? 0, // Lấy giá từ biến thể đầu tiên
                PriceSell = product.Variants.FirstOrDefault()?.PriceSell ?? 0,
                Quantity = product.Variants.FirstOrDefault()?.Quantity ?? 0,
                SizeId = product.Variants.FirstOrDefault()?.SizeId ?? 0,
                ColorId = product.Variants.FirstOrDefault()?.ColorId ?? 0,
                // Trả về danh sách URL của hình ảnh
                ImageUrls = product.Images.Select(i => new ImageDto {Id = i.Id, Url = i.Url }).ToList()
            };
        }
    }
}
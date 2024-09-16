using System.ComponentModel.DataAnnotations;
using API.Data.Enum;
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

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Variant> Variants { get; set; } = new List<Variant>();

    }
}
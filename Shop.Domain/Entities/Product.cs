using Shop.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Entities
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
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

    }
}
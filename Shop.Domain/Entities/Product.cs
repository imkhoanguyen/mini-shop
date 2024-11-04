using Shop.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; } 
        public bool IsDelete { get; set; } = false;
        public ICollection<Variant> Variants { get; set; } = new List<Variant>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
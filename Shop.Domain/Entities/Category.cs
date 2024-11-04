using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsDelete { get; set; } = false;
        public ICollection<ProductCategory>? ProductCategories { get; set; } = new List<ProductCategory>();

    }


}
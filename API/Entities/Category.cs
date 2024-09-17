using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public int ParentId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public bool IsDelete {get ; set; } = false;
        public ICollection<Product>? Products { get; set; }
    }
}
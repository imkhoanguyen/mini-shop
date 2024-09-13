using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace API.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsDelete { get; set; } = false;

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        //public List<Image> Images { get; set; } = new List<Image>();


        //public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
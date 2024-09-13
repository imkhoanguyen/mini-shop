using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class ProductCategory
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class ProductUserLike
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string AppUserId { get; set; }= default!;

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }
    }
}
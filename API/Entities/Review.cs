using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Entities
{
    public class Review : BaseEntity
    {
        [Required]
        public string Comment { get; set; } = null!;
        [Range(1, 5)]
        public int Rating { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public bool isVisible { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        public required string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public AppUser? AppUser { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }
    }
}
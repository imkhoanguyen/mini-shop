using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace API.Entities
{
    public class Image : BaseEntity
    {
        [Required]
        [Url]
        public string Url { get; set; } = null!;
        public bool IsMain { get; set; } = false;
        public string? PublicId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }
         public int ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        [ValidateNever]
        public Review? Review { get; set; }
    }
}
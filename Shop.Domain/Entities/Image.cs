using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Image : BaseEntity
    {
        [Required]
        [Url]
        public string Url { get; set; } = null!;
        public bool IsMain { get; set; } = false;
        public string? PublicId { get; set; }

        public string? AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }

       
    }
}
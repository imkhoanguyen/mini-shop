using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class ReviewImage : BaseEntity
    {
        public string? PublicId { get; set; }
        public string? ImgUrl { get; set; }
        // nav
        public int ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public Review? Review { get; set; }
    }
}

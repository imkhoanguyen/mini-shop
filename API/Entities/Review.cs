using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Entities
{
    public class Review : BaseEntity
    {
        [Required]
        public string ReviewText { get; set; } = null!;
        [Range(1, 5)]
        public int? Rating { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int? ParentReviewId { get; set; }
        public string? VideoUrl { get; set; }
        public string? PublicId { get; set; }

        //nav
        public required string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public Review? ParentReview { get; set; } // Review cha nếu là reply
        public List<Review> Replies { get; set; } = []; // Các reply cho review này

        public List<ReviewImage> Images { get; set; } = []; // list ảnh
    }
}
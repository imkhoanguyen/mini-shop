
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.DTOs.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string ReviewText { get; set; } = null!;
        public int? Rating { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int? ParentReviewId { get; set; }
        public string? VideoUrl { get; set; }
        public UserReview? UserReview { get; set; }
        public int ProductId { get; set; }
        public List<ReviewDto> Replies { get; set; } = [];
        public List<ImgReviewDto> Images { get; set; } = [];


    }
}

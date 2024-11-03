using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Reviews
{
    public class ReviewCreateDto
    {
        public required string ReviewText { get; set; }
        public int? Rating { get; set; }
        public IFormFile? VideoFile { get; set; } = null;
        public required string UserId { get; set; }
        public int ProductId { get; set; }
        public List<IFormFile> ImageFile { get; set; } = [];
        public int? ParentReviewId { get; set; }
    }
}

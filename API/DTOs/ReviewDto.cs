using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
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

        public static ReviewDto FromEntity(Review entity)
        {
            return new ReviewDto
            {
                Id = entity.Id,
                ReviewText = entity.ReviewText,
                Rating = entity.Rating,
                Created = entity.Created,
                ParentReviewId = entity.ParentReviewId,
                VideoUrl = entity.VideoUrl,
                UserReview = new UserReview { Id = entity.UserId, FullName = entity.AppUser.Fullname },
                ProductId = entity.ProductId,
                Replies = entity.Replies.Select(ReviewDto.FromEntity).ToList(),
                Images = entity.Images.Select(ImgReviewDto.FromEntity).ToList(),
            };
        }
    }

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

    public class UserReview
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
    }

    public class ImgReviewDto
    {
        public int Id { get; set; }
        public required string ImgUrl { get; set; }

        public static ImgReviewDto FromEntity(ReviewImage entity)
        {
            return new ImgReviewDto
            {
                Id = entity.Id,
                ImgUrl = entity.ImgUrl
            };
        }
    }

    public class ReviewEditDto()
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public required string ReviewText { get; set; }
    }

    public class RemoveImageDto()
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
    }
}

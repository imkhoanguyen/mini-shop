using Shop.Application.DTOs.Reviews;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class ReviewMapper
    {
        public static ReviewDto EntityToReviewDto(Review entity)
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
                Replies = entity.Replies.Select(EntityToReviewDto).ToList(),
                Images = entity.Images.Select(ReviewImageToImageReviewDto).ToList(),
            };
        }

        public static ImgReviewDto ReviewImageToImageReviewDto(ReviewImage entity)
        {
            return new ImgReviewDto
            {
                Id = entity.Id,
                ImgUrl = entity.ImgUrl
            };
        }
        
    }
}

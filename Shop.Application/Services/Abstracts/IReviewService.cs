using API.Helpers;
using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Reviews;
using Shop.Application.Ultilities;

namespace Shop.Application.Services.Abstracts
{
    public interface IReviewService
    {
        Task<PagedList<ReviewDto>> GetAllAsync(int productId, ReviewParams prm, bool tracked = false);
        Task<ReviewDto> AddAsync(ReviewCreateDto dto);
        Task<ReviewDto> UpdateAsync(ReviewEditDto dto);
        Task<ReviewDto> AddImageAsync(int reviewId, List<IFormFile> files);
        Task RemoveImageAsync(int reviewId, int imageId);
        Task<ReviewDto> AddVideoAsync(int reviewId, IFormFile video);
        Task RemoveVideoAsync(int reviewId);
        Task<ReviewDto> AddReplyAsync(ReplyCreateDto dto);
        Task RemoveReview(int reviewId);

    }
}

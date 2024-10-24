using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IReviewRepository
    {
        void Add(Review review);
        void Delete(Review review);
        Task<PagedList<Review>> GetAllAsync(int productId, ReviewParams prm);

        Task UpdateAsync(ReviewEditDto dto);
        Task<Review?> GetAsync(int reviewId);

    }
}
using API.Entities;

namespace API.Interfaces
{
    public interface IReviewRepository
    {
        void Add(Review review);
        Task<IEnumerable<Review>> GetAllAsync(int productId);
    }
}
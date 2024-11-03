using API.Helpers;
using Shop.Application.DTOs.Reviews;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<PagedList<Review>> GetAllAsync(int productId, ReviewParams prm, bool tracked = false);

        Task UpdateAsync(Review dto);
    }
}
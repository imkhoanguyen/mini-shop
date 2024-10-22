using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly StoreContext _context;

        public ReviewRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync(int productId)
        {
            var query = _context.Reviews.AsQueryable();

            query = query.Include(r => r.AppUser).Include(r => r.ParentReview).Include(r => r.Images).Include(r => r.Replies) 
            .ThenInclude(reply => reply.AppUser);

            query = query.Where(r => r.ProductId == productId && r.ParentReview == null);

            return await query.ToListAsync();
        }

        public async Task<Review?> GetAsync(int reviewId)
        {
            var query = _context.Reviews.AsQueryable();

            query = query.Include(r => r.AppUser).Include(r => r.ParentReview).Include(r => r.Images).Include(r => r.Replies)
            .ThenInclude(reply => reply.AppUser);

            return await query.FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public void Add(Review review)
        {
            _context.Reviews.Add(review);
        }

        public async Task UpdateAsync(ReviewEditDto review)
        {
            var reviewFromDb = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
            if (reviewFromDb != null)
            {
                if(review.Rating != null)
                    reviewFromDb.Rating = review.Rating;
                reviewFromDb.ReviewText = review.ReviewText;
            }
        }
    }
}

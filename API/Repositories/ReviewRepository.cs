using API.Data;
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

            query = query.Include(r => r.AppUser).Include(r => r.ParentReview);

            query = query.Where(r => r.ProductId == productId);

            return await query.ToListAsync();
        }

        public void Add(Review review)
        {
            _context.Reviews.Add(review);
        }
    }
}

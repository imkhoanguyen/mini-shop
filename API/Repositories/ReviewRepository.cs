using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly StoreContext _context;

        public ReviewRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Review>> GetAllAsync(int productId, ReviewParams prm)
        {
            var query = _context.Reviews.AsQueryable();

            query = query.Include(r => r.AppUser).Include(r => r.ParentReview).Include(r => r.Images).Include(r => r.Replies) 
            .ThenInclude(reply => reply.AppUser);

            query = query.Where(r => r.ProductId == productId && r.ParentReview == null);

            if(!prm.OrderBy.IsNullOrEmpty())
            {
                query = prm.OrderBy switch
                {
                    "id" => query.OrderBy(r => r.Id),
                    "id_desc" => query.OrderByDescending(r => r.Id),
                    _ => query.OrderByDescending(r => r.Id),
                };
            }

            if(prm.Rating > 0)
            {
                query = prm.Rating switch
                {
                    1 => query.Where(r => r.Rating == 1),
                    2 => query.Where(r => r.Rating == 2),
                    3 => query.Where(r => r.Rating == 3),
                    4 => query.Where(r => r.Rating == 4),
                    5 => query.Where(r => r.Rating == 5),
                    6 => query.Where(r => r.Images.Count > 0 || r.VideoUrl != null), // have image or video
                    _ => query,
                };
            }

            return await PagedList<Review>.CreateAsync(query, prm.PageNumber, prm.PageSize);
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

        public void Delete(Review review)
        {
            _context.Reviews.Remove(review);    
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

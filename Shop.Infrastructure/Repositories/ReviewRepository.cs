using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.DTOs.Reviews;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Ultilities;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly StoreContext _context;

        public ReviewRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<Review>> GetAllAsync(int productId, ReviewParams prm, bool tracked = false)
        {
            var query = tracked ? _context.Reviews.AsQueryable() : _context.Reviews.AsNoTracking().AsQueryable();

            query = query.Include(r => r.AppUser).Include(r => r.ParentReview).Include(r => r.Images).Include(r => r.Replies)
            .ThenInclude(reply => reply.AppUser);

            query = query.Where(r => r.ProductId == productId && r.ParentReview == null);

            if (!prm.OrderBy.IsNullOrEmpty())
            {
                query = prm.OrderBy switch
                {
                    "id" => query.OrderBy(r => r.Id),
                    "id_desc" => query.OrderByDescending(r => r.Id),
                    _ => query.OrderByDescending(r => r.Id),
                };
            }

            if (prm.Rating > 0)
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

            return await query.ApplyPaginationAsync(prm.PageNumber, prm.PageSize);
        }

        public override async Task<Review?> GetAsync(Expression<Func<Review, bool>> expression, bool tracked = false)
        {
            if(tracked) 
                return await _context.Reviews
                .Include(r => r.AppUser)
                .Include(r => r.ParentReview)
                .Include(r => r.Images)
                .Include(r => r.Replies).ThenInclude(reply => reply.AppUser)
                .FirstOrDefaultAsync(expression);



            return await _context.Reviews
                .Include(r => r.AppUser)
                .Include(r => r.ParentReview)
                .Include(r => r.Images)
                .Include(r => r.Replies).ThenInclude(reply => reply.AppUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }

        public async Task UpdateAsync(Review review)
        {
            var reviewFromDb = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
            if (reviewFromDb != null)
            {
                if (review.Rating != null)
                    reviewFromDb.Rating = review.Rating;
                reviewFromDb.ReviewText = review.ReviewText;
            }
        }

    }
}

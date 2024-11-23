using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Ultilities;
using System.Linq.Expressions;
namespace Shop.Infrastructure.Repositories
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly StoreContext _context;
        public DiscountRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateDiscountAsync(Discount discount)
        {
            var discountDb = await _context.Discounts.FirstOrDefaultAsync(dc => dc.Id == discount.Id);
            if (discountDb is not null)
            {
                discountDb.Name = discount.Name;
                discountDb.AmountOff=discount.AmountOff;
                discountDb.PercentOff=discount.PercentOff;
                discountDb.PromotionCode=discount.PromotionCode;
            
            }
        }
        public async Task<string> GenerateCode(int length = 10)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string code;
            bool isExist;

            do
            {
                code = new string(Enumerable.Repeat(characters, length)
                                            .Select(s => s[random.Next(s.Length)]).ToArray());
                isExist = await _context.Discounts.AnyAsync(dc => dc.PromotionCode == code);
            }
            while (isExist); 
            return code;
        }
        public async Task AddDiscounts(Discount discount)
    {
        discount.PromotionCode=await GenerateCode(10);
        await _context.Discounts.AddAsync(discount);
    }

        public override async Task<Discount?> GetAsync(Expression<Func<Discount, bool>> expression, bool tracked = false)
        {
            var query = _context.Discounts.Where(dc => !dc.IsDelete).AsQueryable();

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(expression);
        }
        
        public async Task<PagedList<Discount>> GetAllDiscountsAsync(DiscountParams discountParams, bool tracked = false)
        {
            var query = tracked ? _context.Discounts.AsQueryable().Where(dc => !dc.IsDelete) 
                : _context.Discounts.AsNoTracking().AsQueryable().Where(dc => !dc.IsDelete);

            if (!string.IsNullOrEmpty(discountParams.Search))
            {
                query = query.Where(dc => dc.Name.ToLower().Contains(discountParams.Search.ToLower())
                    || dc.Id.ToString() == discountParams.Search);
            }

            if (!string.IsNullOrEmpty(discountParams.OrderBy))
            {
                query = discountParams.OrderBy switch
                {
                    "id" => query.OrderBy(dc => dc.Id),
                    "id_desc" => query.OrderByDescending(dc => dc.Id),
                    "name" => query.OrderBy(dc => dc.Name.ToLower()),
                    "name_desc" => query.OrderByDescending(dc => dc.Name.ToLower()),
                    _ => query.OrderByDescending(dc => dc.Id)
                };
            }

            return await query.ApplyPaginationAsync(discountParams.PageNumber,discountParams.PageSize);
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync(bool tracked = false)
        {
            if (tracked)
                return await _context.Discounts.Where(dc => !dc.IsDelete).ToListAsync();
            return await _context.Discounts.AsNoTracking().Where(dc => !dc.IsDelete).ToListAsync();
        }

        public  async Task DeleteDiscountAsync(Discount discount)
        {
            var discountDb = await _context.Discounts.FirstOrDefaultAsync(dc => dc.Id == discount.Id);
            if (discountDb is not null)
            {
                discountDb.IsDelete = true;
            }
        }
    }
}
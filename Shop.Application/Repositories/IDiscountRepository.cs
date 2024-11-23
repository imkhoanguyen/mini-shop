using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<PagedList<Discount>> GetAllDiscountsAsync(DiscountParams discountParams, bool tracked = false);
        Task<IEnumerable<Discount>> GetAllDiscountsAsync(bool tracked = false);
        Task DeleteDiscountAsync(Discount discount);

        Task UpdateDiscountAsync(Discount discount);
        Task AddDiscounts(Discount discount);
        
    }
}
using Shop.Application.DTOs.Discounts;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using System.Linq.Expressions;
namespace Shop.Application.Services.Abstracts
{
    public interface IDiscountService
    {
        Task<PagedList<DiscountDto>> GetAllAsync(DiscountParams discountParams,bool tracked);
        Task<IEnumerable<DiscountDto>> GetAllDiscount(bool tracked);
        Task<DiscountDto?>GetAsync(Expression<Func<Discount,bool>>expression);
        Task <DiscountDto> UpdateAsync(DiscountUpdate  discountUpdate);
        Task <DiscountDto> AddAsync(DiscountAdd discountAdd);
        Task DeleteAsync(Expression<Func<Discount,bool>>expression);
    }
}
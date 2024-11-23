using Shop.Application.DTOs.ShippingMethods;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using System.Linq.Expressions;
namespace Shop.Application.Services.Abstracts
{
    public interface IShippingMethodService
    {
        Task<PagedList<ShippingMethodDto>> GetAllAsync(ShippingMethodParameters shippingMethodParameters,bool tracked);
        Task<IEnumerable<ShippingMethodDto>> GetAllShippingMethod(bool tracked);
        Task<ShippingMethodDto?>GetAsync(Expression<Func<ShippingMethod,bool>>expression);
        Task <ShippingMethodDto> UpdateAsync(ShippingMethodUpdate shippingMethod);
        Task <ShippingMethodDto> AddAsync(ShippingMethodAdd shippignMethod);
        Task DeleteAsync(Expression<Func<ShippingMethod,bool>>expression);
    }
}
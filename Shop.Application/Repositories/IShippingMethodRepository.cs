using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IShippingMethodRepository : IRepository<ShippingMethod>
    {
        Task<PagedList<ShippingMethod>> GetAllShippingMethodAsync(ShippingMethodParameters shippingMethodParameters,bool tracked=false);
        Task UpdateShippingMethodAsync(ShippingMethod shippingMethod); // update some attribute
        Task DeleteShippingMethodAsync(ShippingMethod shippingMethod); // safe delete
        Task<IEnumerable<ShippingMethod>> GetAllShippingMethodsAsync(bool tracked=false);
    }
}
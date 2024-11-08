using Shop.Application.Repositories;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IShippingMethodRepository : IRepository<ShippingMethod>
    {
        Task UpdateShippingMethodAsync(ShippingMethod shippingMethod); // update some attribute
        Task DeleteShippingMethodAsync(ShippingMethod shippingMethod); // safe delete
        Task<IEnumerable<ShippingMethod>> GetAllShippingMethodsAsync(bool tracked);
    }
}
using API.Entities;

namespace API.Interfaces
{
    public interface IShippingMethodRepository
    {
        void AddShippingMethod(ShippingMethod shippingMethod);
        void UpdateShippingMethod(ShippingMethod shippingMethod);
        void DeleteShippingMethod(ShippingMethod shippingMethod);
    }
}
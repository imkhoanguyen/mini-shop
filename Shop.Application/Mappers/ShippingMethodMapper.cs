using Shop.Application.DTOs.ShippingMethods;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class ShippingMethodMapper
    {
        public static ShippingMethod ShippingMethodAddDtoToEntity(ShippingMethodAdd shippingMethod)
        {
            return new ShippingMethod
            {
                Name =shippingMethod.Name,
                Cost=shippingMethod.Cost,
                EstimatedDeliveryTime = shippingMethod.EstimatedDeliveryTime,
                Created= DateTime.UtcNow.AddHours(7),
                Updated=DateTime.UtcNow.AddHours(7)
            };
        }
        public static ShippingMethod ShippingMethodUpdateToEntity(ShippingMethodUpdate shippingMethod)
        {
            return new ShippingMethod
            {
                Id=shippingMethod.Id,
                Name=shippingMethod.Name,
                Cost=shippingMethod.Cost,
                EstimatedDeliveryTime = shippingMethod.EstimatedDeliveryTime,
                Updated=DateTime.UtcNow.AddHours(7),
            };
        }
        public static ShippingMethodDto EntityToShippingMethodDto(ShippingMethod shippingMethod)
        {
            return new ShippingMethodDto
            {
                Id = shippingMethod.Id,
                Name = shippingMethod.Name,
                Cost = shippingMethod.Cost,
                EstimatedDeliveryTime = shippingMethod.EstimatedDeliveryTime,
                Created = shippingMethod.Created,
                Updated = shippingMethod.Updated,
            };
        }
    }
}

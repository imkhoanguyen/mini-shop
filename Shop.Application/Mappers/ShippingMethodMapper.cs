using Shop.Application.DTOs.ShippingMethods;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class ShippingMethodMapper
    {
        public static ShippingMethod ShippingMethodDtoToEntity(ShippingMethodDto shippingMethodDto)
        {
            return new ShippingMethod
            {
                Id = shippingMethodDto.Id,
                Name = shippingMethodDto.Name,
                Cost = shippingMethodDto.Cost,
                EstimatedDeliveryTime = shippingMethodDto.EstimatedDeliveryTime,
                Created = shippingMethodDto.Created,
                Updated = shippingMethodDto.Updated
            };
        }
    }
}

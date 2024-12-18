using Shop.Application.DTOs.Orders;
using Shop.Domain.Entities;
using Shop.Domain.Enum;

namespace Shop.Application.Mappers
{
    public class OrderMapper
    {
        public static Order FromAddDtoToEntity(OrderAddDto dto)
        {
            return new Order
            {
                Address = dto.Address,
                Phone = dto.Phone,
                UserId = dto.UserId,
                FullName = dto.FullName,
                DiscountId = dto.DiscountId,
                DiscountPrice = dto.DiscountPrice,
                ShippingFee = dto.ShippingFee,
                ShippingMethodId = dto.ShippingMethodId,
                SubTotal = dto.SubTotal,
                Description = dto.Description,
                PaymentMethod = Enum.Parse<PaymentMethod>(dto.PaymentMethod),
                StripeSessionId = dto.StripeSessionId
            };
        }

        public static OrderDto FromEntityToDto(Order order)
        {
            return new OrderDto
            {
                Address = order.Address,
                Phone = order.Phone,
                FullName = order.FullName,
                UserId = order.UserId,

                ShippingFee = order.ShippingFee,
                ShippingMethodId = order.ShippingMethodId,
                ShippingName = order.ShippingMethod.Name,
                ShippingMethodDesciption = order.ShippingMethod.EstimatedDeliveryTime,

                DiscountId = order.DiscountId,
                DiscountPrice = order.DiscountPrice,

                Created = order.Created,
                Updated = order.Updated,
                Description = order.Description,
                Status = order.Status.ToString(),
                Id = order.Id,
                SubTotal = order.SubTotal,
                PaymentMethod = order.PaymentMethod.ToString(),
                PaymentStatus = order.PaymentStatus.ToString(),
                OrderItems = order.OrderItems.Select(item => new OrderItemsDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    SizeName=item.SizeName,
                    ColorName=item.ColorName,
                    ProductImage = item.ProductImage,
                    ProductName = item.ProductName,
                }).ToList()
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.DTOs.OrderItems;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class OrderItemsMapper
    {
        public static OrderItems FromAddDtoToEntity(OrderItemsAddDto dto)
        {
            return new OrderItems
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Price = dto.Price,
                ProductName = dto.ProductName,
                OrderId = dto.OrderId,
                SizeName = dto.SizeName,
                ColorName = dto.ColorName,

            };
        }
    }
}
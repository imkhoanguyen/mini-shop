using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.OrderItems;
using Shop.Application.Mappers;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class OrderItemsController : BaseApiController
    {
        private readonly IOrderItemsService _orderItemsService;
        public OrderItemsController(IOrderItemsService orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Addsize(OrderItemsAddDto dto)
        {
            var orderItems = OrderItemsMapper.FromAddDtoToEntity(dto);
            await _orderItemsService.AddAsync(orderItems);

            return Ok(new { message = "add items success" });

        }

    }
}
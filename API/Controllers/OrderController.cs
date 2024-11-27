using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Orders;
using Shop.Application.Mappers;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(OrderAddDto dto)
        {
            var order = OrderMapper.FromAddDtoToEntity(dto);
            await _orderService.AddAsync(order);

            return Ok(order.Id);

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);

                if (orders == null || !orders.Any())
                {
                    return NotFound("No orders found for this user.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("revenue/today")]
        public async Task<IActionResult> GetTodayRevenue()
        {
            var today = DateTime.UtcNow;
            var revenue = await _orderService.GetTotalRevenueByDateAsync(today);
            return Ok(new 
            { 
                date = today.Date, 
                totalRevenue = revenue,
                status = "PaymentReceived"
            });
        }

        [HttpGet("revenue/by-date")]
        public async Task<IActionResult> GetRevenueByDate([FromQuery] DateTime date)
        {
            if (date == null || date == DateTime.MinValue)
            {
                return BadRequest("Invalid date.");
            }

            var revenue = await _orderService.GetTotalRevenueByDateAsync(date);

            return Ok(new
            {
                date = date.Date,
                totalRevenue = revenue,
                status = "PaymentReceived"
            });
        }


        [HttpGet("revenue/monthly")]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery] int year, [FromQuery] int month)
        {
            if (year <= 0 || month <= 0 || month > 12)
            {
                return BadRequest("Invalid year or month.");
            }

            var revenue = await _orderService.GetTotalRevenueByMonthAsync(year, month);
            return Ok(new
            {
                year,
                month,
                totalRevenue = revenue,
                status = "PaymentReceived"
            });
        }

        [HttpGet("revenue/yearly")]
        public async Task<IActionResult> GetYearlyRevenue([FromQuery] int year)
        {
            if (year <= 0)
            {
                return BadRequest("Invalid year.");
            }

            var revenue = await _orderService.GetTotalRevenueByYearAsync(year);
            return Ok(new
            {
                year,
                totalRevenue = revenue,
                status = "PaymentReceived"
            });
        }

        [HttpGet("revenue/allMonthInYear")]
        public async Task<IActionResult> GetMonthlyBreakdownForYear([FromQuery] int year)
        {
            if (year <= 0)
            {
                return BadRequest("Invalid year.");
            }

            var revenues = new List<object>();
            for (int month = 1; month <= 12; month++)
            {
                var revenue = await _orderService.GetTotalRevenueByMonthAsync(year, month);
                revenues.Add(new { month, revenue });
            }

            return Ok(new { year, breakdown = revenues });
        }

        [HttpGet("count-today")]
        public async Task<IActionResult> CountOrdersToday()
        {
            var count = await _orderService.CountOrdersTodayAsync();
            return Ok(new
            {
                date = DateTime.UtcNow.Date,
                count,
                status = "PaymentReceived"
            });
        }
        [HttpGet("count-orders/by-date")]
        public async Task<IActionResult> CountOrdersByDate([FromQuery] DateTime date)
        {
            if (date == null || date == DateTime.MinValue)
            {
                return BadRequest("Invalid date.");
            }

            var count = await _orderService.CountOrdersByDateAsync(date);

            return Ok(new
            {
                date = date.Date,
                count,
                status = "PaymentReceived"
            });
        }
        [HttpGet("count-monthly")]
        public async Task<IActionResult> CountOrdersByMonth([FromQuery] int year, [FromQuery] int month)
        {
            if (year <= 0 || month <= 0 || month > 12)
            {
                return BadRequest("Invalid year or month.");
            }

            var count = await _orderService.CountOrdersByMonthAsync(year, month);
            return Ok(new
            {
                year,
                month,
                count,
                status = "PaymentReceived"
            });
        }
        [HttpGet("count-yearly")]
        public async Task<IActionResult> CountOrdersByYear([FromQuery] int year)
        {
            if (year <= 0)
            {
                return BadRequest("Invalid year.");
            }

            var count = await _orderService.CountOrdersByYearAsync(year);
            return Ok(new
            {
                year,
                count,
                status = "PaymentReceived"
            });
        }
    }
}
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.DTOs.Orders;
using Shop.Application.DTOs.Users;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Exceptions;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders([FromQuery] OrderParams prm)
        {
            if (!prm.UserId.IsNullOrEmpty())
            {
                var userId = ClaimsPrincipleExtensions.GetUserId(User);
                if (userId != prm.UserId)
                {
                    throw new BadRequestException("Tài khoản của người dùng không khớp vui lòng đăng nhập lại");
                }
            }
            var pagedList = await _orderService.GetAllAsync(prm, false);
            Response.AddPaginationHeader(pagedList);
            return Ok(pagedList);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] OrderAddDto dto)
        {
            dto.UserId = ClaimsPrincipleExtensions.GetUserId(User);
            if (!await _orderService.CheckOrderItems(dto))
                throw new BadRequestException("Lỗi số lượng sản phẩm");
            var orderToReturn = await _orderService.AddAsync(dto);
            return Ok(orderToReturn);
        }

        [HttpDelete("{orderId:int}")]
        public async Task<IActionResult> Delete(int orderId)
        {
            await _orderService.DeleteOrderAsync(o => o.Id == orderId);
            return Ok();
        }

        [HttpGet("stripeSessionId/{stripeSessionId}")]
        public async Task<ActionResult<OrderDto>> GetByStripeSessionId(string stripeSessionId)
        {
            if (stripeSessionId.IsNullOrEmpty())
                throw new BadRequestException("stripeSessionId is required");

            return await _orderService.GetAsync(o => o.StripeSessionId == stripeSessionId);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            if (id < 1)
                throw new BadRequestException("id phải lớn hơn 0");

            return await _orderService.GetAsync(o => o.Id == id);
        }

        [HttpPut("{id}/status")]
        [Authorize(Policy = ClaimStore.Order_Comfirm)]
        public async Task<ActionResult<OrderDto>> UpdateStatus(int id, [FromBody] string status)
        {
            if (id < 1)
                throw new BadRequestException("id phải lớn hơn 0");

            var order = await _orderService.UpdateStatus(id, status);
            return Ok(order);
        }

        [HttpPut("{id}/payment-status")]
        [Authorize(Policy = ClaimStore.Order_ComfirmPayment)]
        public async Task<ActionResult<OrderDto>> UpdatePaymentStatus(int id, [FromBody] string paymentStatus)
        {
            if (id < 1)
                throw new BadRequestException("id phải lớn hơn 0");

            var order = await _orderService.UpdatePaymentStatus(id, paymentStatus);
            return Ok(order);
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
                status = "Confirmed",
                paymentStatus = "Paid"
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
                status = "Confirmed",
                paymentStatus = "Paid"
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
                status = "Confirmed",
                paymentStatus = "Paid"
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
                status = "Confirmed",
                paymentStatus = "Paid"
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
                status = "Confirmed",
                paymentStatus = "Paid"

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
                status = "Confirmed",
                paymentStatus = "Paid"
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
                status = "Confirmed",
                paymentStatus = "Paid"
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
                status = "Confirmed",
                paymentStatus = "Paid"
            });
        }
        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetOrdersByYear(int year)
        {
            try
            {
                // Gọi service để lấy danh sách OrderDto
                var orders = await _orderService.GetOrdersByYearAsync(year);

                // Kiểm tra nếu không có order nào
                if (orders == null || orders.Count == 0)
                {
                    return NotFound($"No orders found for the year {year}.");
                }

                // Trả về danh sách order
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu có logger)
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("highest-total/today")]
        public async Task<IActionResult> GetUserWithHighestTotalForToday()
        {
            var result = await _orderService.GetUserWithHighestTotalForTodayAsync();

            if (result.User == null)
                return NotFound(new { message = "No orders found for today." });

            return Ok(new
            {
                User = result.User,
                TotalAmount = result.TotalAmount
            });
        }
        [HttpGet("highest-total-date")]
        public async Task<IActionResult> GetUserWithHighestTotalForDate([FromQuery] DateTime? date)
        {
            var result = await _orderService.GetUserWithHighestTotalForDateAsync(date);

            if (result.User == null)
                return NotFound(new { message = "No orders found for the selected date." });

            return Ok(new
            {
                User = result.User,
                TotalAmount = result.TotalAmount
            });
        }
        [HttpGet("highest-total-month")]
        public async Task<IActionResult> GetUserWithHighestTotalForMonth([FromQuery] int month, [FromQuery] int year)
        {
            var result = await _orderService.GetUserWithHighestTotalForMonthAsync(month, year);

            if (result.User == null)
                return NotFound(new { message = "No orders found for the selected month and year." });

            return Ok(new
            {
                User = result.User,
                TotalAmount = result.TotalAmount
            });
        }

        [HttpGet("highest-total-year")]
        public async Task<IActionResult> GetUserWithHighestTotalForYear([FromQuery] int year)
        {
            var result = await _orderService.GetUserWithHighestTotalForYearAsync(year);

            if (result.User == null)
                return NotFound(new { message = "No orders found for the selected year." });

            return Ok(new
            {
                User = result.User,
                TotalAmount = result.TotalAmount
            });
        }
    }
}
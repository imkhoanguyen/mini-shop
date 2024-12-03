using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using Shop.Application.DTOs.Orders;
using Shop.Application.Mappers;
using Shop.Domain.Exceptions;
using System.Linq.Expressions;
using Shop.Application.Ultilities;
using Shop.Application.Parameters;
using Shop.Application.DTOs.Users;
using Shop.Domain.Enum;

namespace Shop.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;

        public OrderService(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        public async Task<OrderDto> AddAsync(OrderAddDto dto)
        {
            var order = OrderMapper.FromAddDtoToEntity(dto);

            order.OrderItems = dto.Items.Select(item => new OrderItems
            {
                Quantity = item.Quantity,
                Price = item.Price,
                ProductName = item.ProductName,
                ProductId = item.ProductId,
                SizeName = item.SizeName,
                ColorName = item.ColorName,
                ProductImage = item.ProductImage,
            }).ToList();

            await _unitOfWork.OrderRepository.AddAsync(order);

            if (await _unitOfWork.CompleteAsync())
            {
                var orderToReturn = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == order.Id);

                try
                {
                    await _productService.UpdateQuantityProductAsync(orderToReturn);
                    return OrderMapper.FromEntityToDto(orderToReturn);
                }
                catch
                {
                    _unitOfWork.OrderRepository.Remove(order);
                    await _unitOfWork.CompleteAsync(); 

                    throw; 
                }
            }

            throw new BadRequestException("Có lỗi xảy ra khi thêm order.");
        }

        public async Task<PagedList<OrderDto>> GetAllAsync(OrderParams prm, bool tracked = false)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(prm, tracked);
            var orderDtos = orders.Select(OrderMapper.FromEntityToDto).ToList();

            return new PagedList<OrderDto>(orderDtos, orders.TotalCount, orders.CurrentPage, orders.PageSize);
        }

        public async Task<bool> CheckOrderItems(OrderAddDto order)
        {
            foreach (var item in order.Items)
            {
                // check quantity
                var variantOfProduct = await _unitOfWork.VariantRepository.GetAsync(v => v.ProductId == item.ProductId
                && v.Color.Name == item.ColorName && v.Size.Name == item.SizeName && v.Status == Domain.Enum.VariantStatus.Public
                );

                if (variantOfProduct == null)
                {
                    throw new BadRequestException($"Không tìm thấy loại sản phẩm có tên {item.ProductName}. Vui lòng xóa khỏi giỏ hàng và kiểm tra lại");
                }

                if (item.Quantity > variantOfProduct.Quantity)
                {
                    throw new BadRequestException($"Số lượng sản phẩm có tên {item.ProductName} chỉ còn lại {variantOfProduct.Quantity}. Vui lòng điều chỉnh lại số lượng sản phẩm");
                }
            }
            return true;
        }

        public async Task DeleteOrderAsync(Expression<Func<Order, bool>> expression)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(expression);
            if (order == null)
            {
                throw new BadRequestException("Không tìm thấy order");
            }
            _unitOfWork.OrderRepository.Remove(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                throw new BadRequestException("Problem remove order");
            }
        }


        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID must not be null or empty.", nameof(userId));
            }

            // Lấy danh sách Order từ repository
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);

            // Ánh xạ từ Order sang OrderDto
            return orders.Select(OrderMapper.FromEntityToDto).ToList();
        }

        public async Task<decimal> GetTotalRevenueByDateAsync(DateTime date)
        {
            return await _unitOfWork.OrderRepository.GetTotalRevenueByDateAsync(date);
        }

        public async Task<decimal> GetTotalRevenueByMonthAsync(int year, int month)
        {
            return await _unitOfWork.OrderRepository.GetTotalRevenueByMonthAsync(year, month);
        }
        public async Task<decimal> GetTotalRevenueByYearAsync(int year)
        {
            return await _unitOfWork.OrderRepository.GetTotalRevenueByYearAsync(year);
        }
        public async Task<int> CountOrdersTodayAsync()
        {
            return await _unitOfWork.OrderRepository.CountOrdersTodayAsync();
        }
        public async Task<int> CountOrdersByDateAsync(DateTime date)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByDateAsync(date);
        }
        public async Task<int> CountOrdersByMonthAsync(int year, int month)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByMonthAsync(year, month);
        }
        public async Task<int> CountOrdersByYearAsync(int year)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByYearAsync(year);
        }

        public async Task<OrderDto> GetAsync(Expression<Func<Order, bool>> expression, bool tracked = false)
        {
            var order =  await _unitOfWork.OrderRepository.GetAsync(expression, tracked);

            if (order == null)
                throw new NotFoundException("Không tìm thấy đơn hàng");

            return OrderMapper.FromEntityToDto(order);
        }
        public async Task<List<OrderDto>> GetOrdersByYearAsync(int year)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByYearAsync(year);

            // Ánh xạ từ Order sang OrderDto
            return orders.Select(OrderMapper.FromEntityToDto).ToList();
        }

        public async Task<(UserDto? User, decimal TotalAmount)> GetUserWithHighestTotalForTodayAsync()
        {
            var result = await _unitOfWork.OrderRepository.GetUserWithHighestTotalForTodayAsync();

            if (result.User == null)
                return (null, 0);

            var userDto = new UserDto
            {
                Id = result.User.Id,
                FullName = result.User.FullName,
                UserName = result.User.UserName,
                Email = result.User.Email,
                Avatar = result.User.Avatar,
            };

            return (userDto, result.TotalAmount);
        }
        public async Task<(UserDto? User, decimal TotalAmount)> GetUserWithHighestTotalForDateAsync(DateTime? date)
        {
            var result = await _unitOfWork.OrderRepository.GetUserWithHighestTotalForDateAsync(date);

            if (result.User == null)
                return (null, 0);

            var userDto = new UserDto
            {
                Id = result.User.Id,
                FullName = result.User.FullName,
                UserName = result.User.UserName,
                Email = result.User.Email,
                Avatar = result.User.Avatar,
            };

            return (userDto, result.TotalAmount);
        }

        public async Task<(UserDto? User, decimal TotalAmount)> GetUserWithHighestTotalForMonthAsync(int month, int year)
        {
            var result = await _unitOfWork.OrderRepository.GetUserWithHighestTotalForMonthAsync(month, year);

            if (result.User == null)
                return (null, 0);

            var userDto = new UserDto
            {
                Id = result.User.Id,
                FullName = result.User.FullName,
                UserName = result.User.UserName,
                Email = result.User.Email,
                Avatar = result.User.Avatar,
            };

            return (userDto, result.TotalAmount);
        }
        public async Task<(UserDto? User, decimal TotalAmount)> GetUserWithHighestTotalForYearAsync(int year)
        {
            var result = await _unitOfWork.OrderRepository.GetUserWithHighestTotalForYearAsync(year);

            if (result.User == null)
                return (null, 0);

            var userDto = new UserDto
            {
                Id = result.User.Id,
                FullName = result.User.FullName,
                UserName = result.User.UserName,
                Email = result.User.Email,
                Avatar = result.User.Avatar,
            };

            return (userDto, result.TotalAmount);
        }

        public async Task<OrderDto> UpdateStatus(int id, string status)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id, true);
            if (Enum.TryParse<OrderStatus>(status, true, out var statusEnum))
            {
                order.Status = statusEnum;

                if(await _unitOfWork.CompleteAsync())
                {
                    return OrderMapper.FromEntityToDto(order);
                }
                throw new BadRequestException("Problem update order status");
            }
            else
            {
                throw new BadRequestException("Invalid status value");
            }
        }

        public async Task<OrderDto> UpdatePaymentStatus(int id, string status)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id, true);
            if (Enum.TryParse<PaymentStatus>(status, true, out var statusEnum))
            {
                order.PaymentStatus = statusEnum;

                if (await _unitOfWork.CompleteAsync())
                {
                    return OrderMapper.FromEntityToDto(order);
                }
                throw new BadRequestException("Problem update payment status");
            }
            else
            {
                throw new BadRequestException("Invalid status value");
            }
        }
    }
}
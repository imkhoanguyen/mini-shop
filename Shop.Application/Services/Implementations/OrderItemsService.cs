using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Implementations
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderItemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(OrderItems orderItems)
        {
            if (orderItems == null)
            {
                throw new ArgumentNullException(nameof(orderItems), "order items not null");
            }
            await _unitOfWork.OrderItemsRepository.AddAsync(orderItems);
            await _unitOfWork.CompleteAsync();
        }
    }
}
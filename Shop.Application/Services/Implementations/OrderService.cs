using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "order not null");
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();
        }
    }
}
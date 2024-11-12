using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Implementations
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShippingMethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<ShippingMethod>> GetShippingMethodsAsync(bool tracked)
        {
            return _unitOfWork.ShippingMethodRepository.GetAllShippingMethodsAsync(tracked);
        }
    }
}
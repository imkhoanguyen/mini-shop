using Shop.Application.DTOs.ShippingMethods;
using Shop.Application.Mappers;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using System.Linq.Expressions;

namespace Shop.Application.Services.Implementations
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShippingMethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ShippingMethodDto> AddAsync(ShippingMethodAdd shippingMethodAdd)
        {
            if (await _unitOfWork.ShippingMethodRepository.ExistsAsync(sm=>sm.Name.ToLower()==shippingMethodAdd.Name.ToLower()))
            {
                throw new BadRequestException("Phương thức đã tồn tại");
            }
            var shippignMethod=ShippingMethodMapper.ShippingMethodAddDtoToEntity(shippingMethodAdd);
            await _unitOfWork.ShippingMethodRepository.AddAsync(shippignMethod);
            return await _unitOfWork.CompleteAsync()
                ? ShippingMethodMapper.EntityToShippingMethodDto(shippignMethod)
                : throw new BadRequestException("Thêm phương thức thất bại");
        }

        public async Task DeleteAsync(Expression<Func<ShippingMethod, bool>> expression)
        {
            var shippignMethod= await _unitOfWork.ShippingMethodRepository.GetAsync(expression);
            if(shippignMethod is null)
                throw new NotFoundException("Phương thức không tồn tại");
            await _unitOfWork.ShippingMethodRepository.DeleteShippingMethodAsync(shippignMethod);
            if(!await _unitOfWork.CompleteAsync())
                throw new BadRequestException("Xóa phương thức thất bại");
        }

        public async Task<PagedList<ShippingMethodDto>> GetAllAsync(ShippingMethodParameters shippingMethodParameters, bool tracked)
        {
            var shippignMethod=await _unitOfWork.ShippingMethodRepository.GetAllShippingMethodAsync(shippingMethodParameters,tracked);
            var shippingMethodDto=shippignMethod.Select(ShippingMethodMapper.EntityToShippingMethodDto);
            return new PagedList<ShippingMethodDto>(shippingMethodDto,shippignMethod.TotalCount,shippingMethodParameters.PageNumber,shippingMethodParameters.PageSize);
        }

        public async Task<IEnumerable<ShippingMethodDto>> GetAllShippingMethod(bool tracked)
        {
            var shippingMethod= await _unitOfWork.ShippingMethodRepository.GetAllShippingMethodsAsync(tracked);
            return shippingMethod.Select(ShippingMethodMapper.EntityToShippingMethodDto);
        }
        public async Task<ShippingMethodDto?> GetAsync(Expression<Func<ShippingMethod,bool>>expression)
        {
            var shippignMethod =await _unitOfWork.ShippingMethodRepository.GetAsync(expression);
            if (shippignMethod is null) 
                throw new NotFoundException("Phuơng thức không tồn tại");
            return ShippingMethodMapper.EntityToShippingMethodDto(shippignMethod);
        }
        public async Task<ShippingMethodDto> UpdateAsync(ShippingMethodUpdate shippingMethodUpdate)
        {
            if(!await _unitOfWork.ShippingMethodRepository.ExistsAsync(sm=>sm.Id==shippingMethodUpdate.Id))
                throw new NotFoundException("Phương thức không tồn tại");
    
            if (await _unitOfWork.ShippingMethodRepository.ExistsAsync(sm=>sm.Name.ToLower()==shippingMethodUpdate.Name.ToLower()&& sm.Id!=shippingMethodUpdate.Id))
            {
                throw new BadRequestException("Phương thức đã tồn tại");
            }
            var shippignMethod=ShippingMethodMapper.ShippingMethodUpdateToEntity(shippingMethodUpdate);
            await _unitOfWork.ShippingMethodRepository.UpdateShippingMethodAsync(shippignMethod);
            return await _unitOfWork.CompleteAsync()
                ? ShippingMethodMapper.EntityToShippingMethodDto(shippignMethod)
                : throw new BadRequestException("Cập nhật phương thức thất bại");
        }
    }
}
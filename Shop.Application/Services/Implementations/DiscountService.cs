using Shop.Application.DTOs.Discounts;
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
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unit;

        public DiscountService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<DiscountDto> AddAsync(DiscountAdd discountAdd)
        {
            if(await _unit.DiscountRepository.ExistsAsync(dc => dc.Name.ToLower() == discountAdd.Name.ToLower()))
            {
                throw new BadRequestException("Danh mục đã tồn tại");
            }
            var discount = DiscountMapper.DiscountAddDtoToEntity(discountAdd);
            await _unit.DiscountRepository.AddDiscounts(discount);

            return await _unit.CompleteAsync() 
                ? DiscountMapper.EntityToDiscountDto(discount) 
                : throw new BadRequestException("Thêm danh mục thất bại");

        }

        public async Task DeleteAsync(Expression<Func<Discount, bool>> expression)
        {
            var discount = await _unit.DiscountRepository.GetAsync(expression);
            if (discount is null)
            {
                throw new NotFoundException("Discount không tồn tại");
            }
            await _unit.DiscountRepository.DeleteDiscountAsync(discount);
            if(!await _unit.CompleteAsync())
            {
                throw new BadRequestException("Xóa danh mục thất bại");
            }
        }

        public async Task<PagedList<DiscountDto>> GetAllAsync(DiscountParams discountParams, bool tracked)
        {
            var discount = await _unit.DiscountRepository.GetAllDiscountsAsync(discountParams, tracked);
            
            var discountDto = discount.Select(DiscountMapper.EntityToDiscountDto);
            return new PagedList<DiscountDto>(discountDto, discount.TotalCount, discountParams.PageNumber, discountParams.PageSize);
        }

        public async Task<IEnumerable<DiscountDto>> GetAllDiscount(bool tracked)
        {
            var discounts = await _unit.DiscountRepository.GetAllDiscountsAsync(tracked);
            return discounts.Select(DiscountMapper.EntityToDiscountDto);
        }

        public async Task<DiscountDto?> GetAsync(Expression<Func<Discount, bool>> expression)
        {
            var discount = await _unit.DiscountRepository.GetAsync(expression);
            if(discount is null) throw new NotFoundException("Discount không tồn tại");

            return DiscountMapper.EntityToDiscountDto(discount);
        }

        public async Task<DiscountDto> UpdateAsync(DiscountUpdate discountUpdate)
        {
            if(!await _unit.DiscountRepository.ExistsAsync(dc => dc.Id == discountUpdate.Id))
                throw new NotFoundException("Danh mục không tồn tại");

            if(await _unit.DiscountRepository.ExistsAsync(dc => dc.Name.ToLower() == discountUpdate.Name.ToLower()&& dc.Id!=discountUpdate.Id))
                throw new BadRequestException("Danh mục đã tồn tại");
            
            var discount = DiscountMapper.DiscountUpdateDtoToEntity(discountUpdate);
            await _unit.DiscountRepository.UpdateDiscountAsync(discount);

            return await _unit.CompleteAsync()
                ? DiscountMapper.EntityToDiscountDto(discount)
                : throw new BadRequestException("Cập nhật danh mục thất bại");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using Shop.Application.DTOs.Address;
using Shop.Application.Mappers;

namespace Shop.Application.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AddressDto>> GetAddressByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID must not be null or empty.", nameof(userId));
            }

            var address = await _unitOfWork.AddressRepository.GetAddressByUserIdAsync(userId);

            // Ánh xạ từ Order sang OrderDto
            return address.Select(AddressMapper.FromEntityToDto).ToList();
        }
        
        public async Task<AddressDto> AddAddressAsync(AddressAddDto addressAddDto)
        {
            // if (addressAddDto == null)
            // {
            //     throw new ArgumentNullException(nameof(addressAddDto), "Address information cannot be null.");
            // }

            // // Map DTO to Entity
            // var address = AddressMapper.FromDtoToEntity(addressAddDto);

            // // Add address using repository
            // await _unitOfWork.AddressRepository.AddAsync(address);

            // // Commit changes to the database
            // await _unitOfWork.CompleteAsync();

            // // Return the added address as a DTO
            // return AddressMapper.FromEntityToDto(address);
            if (addressAddDto == null)
            {
                throw new ArgumentNullException(nameof(addressAddDto), "Address information cannot be null.");
            }

            // Map DTO to Entity
            var address = AddressMapper.FromDtoToEntity(addressAddDto);

            // Kiểm tra xem người dùng đã có địa chỉ này chưa (nếu cần)
            var existingAddress = await _unitOfWork.AddressRepository.GetAddressByUserIdAsync(addressAddDto.AppUserId);
            if (existingAddress.Any(a => a.City == addressAddDto.City && a.District == addressAddDto.District && a.Street == addressAddDto.Street))
            {
                throw new InvalidOperationException("This address already exists for the user.");
            }

            // Thêm địa chỉ vào cơ sở dữ liệu
            await _unitOfWork.AddressRepository.AddAsync(address);

            // Commit thay đổi vào cơ sở dữ liệu
            await _unitOfWork.CompleteAsync();

            // Trả về địa chỉ đã được thêm dưới dạng DTO
            return AddressMapper.FromEntityToDto(address);
        }

            public async Task<AddressDto> GetByAddressIdAsync(int addressId)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(addressId);
            if (address == null)
            {
                throw new ArgumentException("Address not found.", nameof(addressId));
            }

            return AddressMapper.FromEntityToDto(address);
        }

        public async Task<AddressDto> UpdateAddressAsync(int addressId, AddressAddDto addressAddDto)
        {
            if (addressAddDto == null)
            {
                throw new ArgumentNullException(nameof(addressAddDto), "Address information cannot be null.");
            }

            var address = await _unitOfWork.AddressRepository.GetByIdAsync(addressId);
            if (address == null)
            {
                throw new ArgumentException("Address not found.", nameof(addressId));
            }

            address.City = addressAddDto.City;
            address.District = addressAddDto.District;
            address.Street = addressAddDto.Street;
            address.AppUserId = addressAddDto.AppUserId;

            await _unitOfWork.AddressRepository.UpdateAsync(address);
            await _unitOfWork.CompleteAsync();

            return AddressMapper.FromEntityToDto(address);
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(addressId);
            if (address == null)
            {
                throw new ArgumentException("Address not found.", nameof(addressId));
            }

            await _unitOfWork.AddressRepository.DeleteAsync(address);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
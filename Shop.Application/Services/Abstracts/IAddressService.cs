using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.Application.DTOs.Address;


namespace Shop.Application.Services.Abstracts
{
    public interface IAddressService
    {
        Task<List<AddressDto>> GetAddressByUserIdAsync(string userId);
        Task<AddressDto> AddAddressAsync(AddressAddDto addressAddDto);
        Task<AddressDto> GetByAddressIdAsync(int addressId);
        Task<AddressDto> UpdateAddressAsync(int addressId, AddressAddDto addressAddDto);
        Task<bool> DeleteAddressAsync(int addressId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.DTOs.Address;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class AddressMapper
    {
        

        public static AddressDto FromEntityToDto(Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                City = address.City,
                District = address.District,
                Street = address.Street,
                AppUserId = address.AppUserId
                // AppUser = address.AppUser
            };
        }
        public static Address FromDtoToEntity(AddressAddDto addressAddDto)
        {
            return new Address
            {
                City = addressAddDto.City,
                District = addressAddDto.District,
                Street = addressAddDto.Street,
                AppUserId = addressAddDto.AppUserId
            };
        }

    }
}
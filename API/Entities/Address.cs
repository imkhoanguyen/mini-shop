using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Entities
{
    public class Address : BaseEntity
    {
        public string? FullName { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public static AddressDto toAddressDto(Address address)
        {
            return new AddressDto
            {
                FullName = address.FullName,
            };
        }
    }
}
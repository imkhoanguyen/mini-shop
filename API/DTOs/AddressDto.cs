using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class AddressDto
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? AddressLine { get; set; }

        public static Address toAddress(AddressDto addressDto)
        {
            return new Address
            {
                FullName = addressDto.FullName,
            };
        }
    }
}
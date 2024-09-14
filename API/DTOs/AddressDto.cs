using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AddressDto
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? AddressLine { get; set; }

    }
}
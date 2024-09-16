using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ColorDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
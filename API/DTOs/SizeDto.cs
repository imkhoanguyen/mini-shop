using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class SizeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public static Size toSize(SizeDto sizeDto)
        {
            return new Size
            {
                Id = sizeDto.Id,
                Name = sizeDto.Name
            };
        }
    }
    public class SizeAddDto
    {
        public string Name { get; set; } = null!;

        public static Size toSize(SizeAddDto sizeAddDto)
        {
            return new Size
            {
                Name = sizeAddDto.Name
            };
        }
    }
}
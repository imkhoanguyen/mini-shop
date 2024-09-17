using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public static Color toColor(ColorDto colorDto)
        {
            return new Color
            {
                Id = colorDto.Id,
                Name = colorDto.Name,
                Code = colorDto.Code
            };
        }
    }
    public class ColorAddDto
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public static Color toColor(ColorAddDto colorAddDto)
        {
            return new Color
            {
                Name = colorAddDto.Name,
                Code = colorAddDto.Code
            };
        }
    }
}
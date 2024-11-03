using Shop.Application.DTOs.Colors;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class ColorMapper
    {
        public static Color ColorAddDtoToEntity(ColorAddDto colorAddDto)
        {
            return new Color
            {
                Name = colorAddDto.Name,
                Code = colorAddDto.Code
            };
        }

        public static Color ColorDtoToEntity(ColorDto colorDto)
        {
            return new Color
            {
                Id = colorDto.Id,
                Name = colorDto.Name,
                Code = colorDto.Code
            };
        }

        public static ColorDto EntityToColorDto(Color color)
        {
            return new ColorDto
            {
                Id = color.Id,
                Name = color.Name,
                Code = color.Code
            };
        }
    }
}

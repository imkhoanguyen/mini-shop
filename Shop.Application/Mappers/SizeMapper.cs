using Shop.Application.DTOs.Size;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class SizeMapper
    {
        public static Size SizeDtoToEntity(SizeDto sizeDto)
        {
            return new Size
            {
                Id = sizeDto.Id,
                Name = sizeDto.Name
            };
        }

        public static Size SizeAddDtoToEntity(SizeAddDto sizeAddDto)
        {
            return new Size
            {
                Name = sizeAddDto.Name
            };
        }

        public static SizeDto EntityToSizeDto(Size size)
        {
            return new SizeDto
            {
                Id = size.Id,
                Name = size.Name
            };
        }
    }
}

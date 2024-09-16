using API.DTOs;

namespace API.Entities
{
    public class Color : BaseEntity
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool IsDelete { get; set; } = false;

        public static ColorDto toColorDto(Color color)
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

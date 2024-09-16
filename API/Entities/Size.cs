using API.DTOs;

namespace API.Entities
{
    public class Size : BaseEntity
    {
        public required string Name { get; set; }
        public bool IsDelete { get; set; } = false;
        public static SizeDto toSizeDto(Size size)
        {
            return new SizeDto
            {
                Id = size.Id,
                Name = size.Name
            };
        }
    }
}

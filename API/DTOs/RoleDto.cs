using API.Entities;

namespace API.DTOs
{
    public class RoleDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        // Mapper
        public static RoleDto FromEntity(AppRole appRole)
        {
            return new RoleDto
            {
                Id = appRole.Id,
                Name = appRole.Name,
                Description = appRole.Description
            };
        }
    }
}

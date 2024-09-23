using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RoleDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Created { get; set; }

        // Mapper
        public static RoleDto FromEntity(AppRole appRole)
        {
            return new RoleDto
            {
                Id = appRole.Id,
                Name = appRole.Name,
                Description = appRole.Description,
                Created = appRole.Created,
            };
        }
    }

    public class RoleCreateDto
    {
        [Required(ErrorMessage ="Tên quyền không được trống")]
        public required string Name { get; set; }
        [Required(ErrorMessage ="Mô tả không được trống")]
        public required string Description { get; set; }
    }
}

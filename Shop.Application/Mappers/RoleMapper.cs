using Shop.Application.DTOs.Roles;
using Shop.Domain.Entities;


namespace Shop.Application.Mappers
{
    public class RoleMapper
    {
        public static RoleDto EntityToRoleDto(AppRole appRole)
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
}

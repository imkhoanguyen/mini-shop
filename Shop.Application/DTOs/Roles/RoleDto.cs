using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.DTOs.Roles
{
    public class RoleDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Created { get; set; }

        
    }

    
}

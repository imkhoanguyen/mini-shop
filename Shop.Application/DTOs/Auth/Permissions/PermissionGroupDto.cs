namespace Shop.Application.DTOs.Auth.Permissions
{
    public class PermissionGroupDto
    {
        public required string GroupName { get; set; }
        public List<PermissionItemDto> Permissions { get; set; } = [];
    }
}

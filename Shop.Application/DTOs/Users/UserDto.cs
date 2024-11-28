namespace Shop.Application.DTOs.Users
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public string? Token { get; set; }
        public bool IsLocked { get; set; }
    }
}
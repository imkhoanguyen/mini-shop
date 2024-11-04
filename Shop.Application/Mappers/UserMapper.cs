using Shop.Application.DTOs.Users;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class UserMapper
    {
        public static UserDto EntityToUserDto(AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
            };
        }
    }
}

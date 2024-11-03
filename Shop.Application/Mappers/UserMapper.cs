using Shop.Application.DTOs.Users;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Mappers
{
    public class UserMapper
    {
        public static UserDto EntityToUserDto(AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Fullname = user.Fullname,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
            };
        }
    }
}

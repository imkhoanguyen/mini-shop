using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        //private readonly IUserRepository _userRepository;
        //public UserController(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}
        //[HttpGet("AdminRoleUsers")]
        //public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersWithAdminRole()
        //{
        //    var users = await _userRepository.GetUsersWithAdminRole();
        //    var userDtos = users.Select(user => AppUser.toUserDto(user)).ToList();
        //    return Ok(userDtos);
        //}
        //[HttpGet("CustomerRoleUsers")]
        //public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersWithCustomerRole()
        //{
        //    var users = await _userRepository.GetUsersWithCustomerRole();
        //    var userDtos = users.Select(user => AppUser.toUserDto(user)).ToList();
        //    return Ok(userDtos);
        //}
    }
}
using API.DTOs;
using API.Entities;
using API.Errors;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ITokenService _tokenService;
            private readonly IUnitOfWork _unitOfWork;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            if (user == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromForm]LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            AppUser? user;
            if (loginDto.UserNameOrEmail!.Contains('@'))
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            }

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.IsLockedOut)
            {
                return Unauthorized("User account is locked out.");
            }

            if (result.Succeeded)
            {
                return new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                };
            }

            return Unauthorized(new ApiResponse(401));
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                { Errors = new[] { "Email address is in use" } });
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
           if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

    }
}
using API.DTOs;
using API.Entities;
using API.Errors;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IUnitOfWork unitOfWork, IEmailService emailService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
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

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.IsLockedOut)
            {
                return Unauthorized("User  account is locked out.");
            }

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Id = user.Id,
                Fullname = user.Fullname,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
           if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var emailExistsResult = await CheckEmailExistsAsync(registerDto.Email!);
            if (emailExistsResult.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                { Errors = new[] { "Email address is in use" } 
                });
            }
            var user = new AppUser
            {
                
                Fullname = registerDto.Fullname,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded) return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Fullname = user.Fullname,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        [HttpGet("forget-password")]
        public async Task<IActionResult> ForgetPassword(CancellationToken cancellationToken, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest("Email không tồn tại");

            string host = _configuration.GetValue<string>("ApplicationUrl");

            string tokenConfirm = await _userManager.GeneratePasswordResetTokenAsync(user);

            string decodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenConfirm));

            string resetPasswordUrl = $"{host}/reset-password?email={email}&token={decodedToken}";

            string body = $"Để reset password của bạn vui lòng click vào đây: <a href=\"{resetPasswordUrl}\">link</a>";


            await _emailService.SendMailAsync(cancellationToken, new EmailRequest
            {
                To = user.Email,
                Subject = "Reset Your Password ",
                Content = body,
            });

            return Ok(new { message = "Vui lòng kiểm tra email" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return BadRequest("Email không tồn tại");

            if (string.IsNullOrEmpty(resetPasswordDto.Token))
                return BadRequest("Không có token");

            string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.Token));

            var identityResult = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.Password);

            if (identityResult.Succeeded)
            {
                return Ok(new { message = "Reset password thành công" });
            }
            else
            {
                return BadRequest(identityResult.Errors.ToList()[0].Description);
            }
        }
    }
}
using API.DTOs;
using API.Entities;
using API.Errors;
using API.Extensions;
using API.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
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
    
        [HttpPost("login/google")]
        public async Task<IActionResult> GoogleResponse([FromBody] TokenRequest tokenRequest)
        {
            var token = tokenRequest.Token;
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    Fullname = payload.Name,
                    Avatar = payload.Picture
                };
                await _userManager.CreateAsync(user);
            }
            return Ok(new UserDto
            {
                Id = user.Id,
                Fullname = user.Fullname,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                Token = await _tokenService.CreateToken(user)
            });
        }
        [HttpGet("validate-facebook-token")]
        public async Task<FacebookPayload> ValidateFacebookToken(string token)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://graph.facebook.com/me?access_token={token}&fields=id,name,email,picture.width(800).height(800)");
            return JsonConvert.DeserializeObject<FacebookPayload>(result)!;
            
        }
        [HttpPost("login/facebook")]
        public async Task<IActionResult> FacebookResponse([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Vui lòng kiểm tra thông tin đăng nhập."));
            var token = tokenRequest.Token;
            var payload = await ValidateFacebookToken(token); 

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    Fullname = payload.Name,
                    Avatar = payload.Picture.Data.Url
                };
                await _userManager.CreateAsync(user);
            }

            return Ok(new UserDto
            {
                Id = user.Id,
                Fullname = user.Fullname,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                Token = await _tokenService.CreateToken(user)
            });
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Vui lòng kiểm tra thông tin đăng nhập."));
            AppUser? user;
            if (loginDto.UserNameOrEmail!.Contains('@'))
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            else
                user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if (user == null) return BadRequest(new ApiResponse(400, "Không tìm thấy tài khoản. Vui lòng kiểm tra lại."));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.IsLockedOut) return Unauthorized("User  account is locked out.");
            if (!result.Succeeded) return BadRequest(new ApiResponse(400, "Mật khẩu không đúng. Vui lòng kiểm tra lại."));

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
            if (!IsPasswordValid(registerDto.Password))
            {
                return BadRequest(new ApiValidationErrorResponse
                { Errors = new[] { "Mật khẩu phải dài ít nhất 6 ký tự, bao gồm một chữ hoa, một chữ thường, một số và một ký tự đặc biệt." } });
            }
            var emailExistsResult = await CheckEmailExistsAsync(registerDto.Email!);
            var usernameExistsResult = await CheckUsernameExistsAsync(registerDto.UserName!);
            if (emailExistsResult.Value)
            {
                return BadRequest(new ApiResponse(400, "Email đã tồn tại."));
            }
            if (usernameExistsResult)
            {
                return BadRequest(new ApiResponse(400, "Username đã tồn tại."));
            }
            var user = new AppUser
            {
                Fullname = registerDto.Fullname,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400, "User registration failed. Please check your details."));

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded) return BadRequest(new ApiResponse(400, "Failed to assign role to user."));

            return new UserDto
            {
                Fullname = user.Fullname,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                Token = await _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> CheckUsernameExistsAsync(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
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

        private bool IsPasswordValid(string password)
        {
            if (password.Length < 6)
                return false;

            if (!password.Any(char.IsLower)) // Kiểm tra có chứa chữ thường
                return false;

            if (!password.Any(char.IsUpper)) // Kiểm tra có chứa chữ hoa
                return false;

            if (!password.Any(char.IsDigit)) // Kiểm tra có chứa số
                return false;

            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) // Kiểm tra có ký tự đặc biệt
                return false;

            return true;
        }
        
    }
}
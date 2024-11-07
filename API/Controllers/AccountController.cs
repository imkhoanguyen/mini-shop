
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Shop.Application.DTOs;
using Shop.Application.DTOs.Auth;
using Shop.Application.DTOs.Auth.Facebook;
using Shop.Application.DTOs.Users;
using Shop.Application.Mappers;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using System.Text;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IEmailService emailService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] TokenRequest tokenRequest)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenRequest.Token);
            var user = await _userManager.FindByEmailAsync(payload.Email) ?? new AppUser
            { 
                Email = payload.Email,
                UserName = payload.Email,
                FullName = payload.Name,
                Avatar = payload.Picture
            };
            if (user.Id == null)
                await _userManager.CreateAsync(user);
            var userDto = UserMapper.EntityToUserDto(user);
            userDto.Token = await _tokenService.CreateToken(user);
            return Ok(userDto);
        }
        [HttpGet("validate-facebook-token")]
        public async Task<FacebookPayload> ValidateFacebookToken(string token)
        {
            var response = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?access_token={token}&fields=id,name,email,picture.width(800).height(800)");
            return JsonConvert.DeserializeObject<FacebookPayload>(response)!;

        }
        [HttpPost("login/facebook")]
        public async Task<IActionResult> FacebookLogin([FromBody] TokenRequest tokenRequest)
        {
            var payload = await ValidateFacebookToken(tokenRequest.Token!);

            var user = await _userManager.FindByEmailAsync(payload.Email!) ?? new AppUser
            {
                Email = payload.Email,
                UserName = payload.Email,
                FullName = payload.Name,
                Avatar = payload.Picture!.Data!.Url
            };

            if (user.Id == null)
                await _userManager.CreateAsync(user);
            var userDto = UserMapper.EntityToUserDto(user);
            userDto.Token = await _tokenService.CreateToken(user);
            return Ok(userDto);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail!) ??
                       await _userManager.FindByNameAsync(loginDto.UserNameOrEmail!);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (user == null || !signInResult.Succeeded)
                return BadRequest("Đăng nhập không hợp lệ");
            var userDto = UserMapper.EntityToUserDto(user);
            userDto.Token = await _tokenService.CreateToken(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (!IsPasswordValid(registerDto.Password))
                return BadRequest("Mật khẩu phải dài ít nhất 6 ký tự, bao gồm một chữ hoa, một chữ thường, một số và một ký tự đặc biệt.");
            
            var user = new AppUser
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest("Đăng ký không thành công");

            await _userManager.AddToRoleAsync(user, "Customer");
            var userDto = UserMapper.EntityToUserDto(user);
            userDto.Token = await _tokenService.CreateToken(user);
            return Ok(userDto);
        }
        private async Task<bool> CheckUsernameExistsAsync(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }
        [HttpGet("email-exists")]
        public async Task<bool> EmailExists([FromQuery] string email) => await _userManager.FindByEmailAsync(email) != null;


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
            return  password.Length >= 6 && 
                    password.Any(char.IsUpper) && 
                    password.Any(char.IsLower) && 
                    password.Any(char.IsDigit) && 
                    password.Any(char.IsPunctuation);
        }

      

    }
}
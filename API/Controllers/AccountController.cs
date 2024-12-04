
using API.Extensions;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shop.Application.DTOs.Auth;
using Shop.Application.DTOs.Auth.Facebook;
using Shop.Application.DTOs.Users;
using Shop.Application.Interfaces;
using Shop.Application.Mappers;
using Shop.Application.Parameters;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
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
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMessageService _service;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IEmailService emailService, ICloudinaryService cloudinaryService, IConfiguration configuration, IMessageService service)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _httpClient = new HttpClient();
            _configuration = configuration;
            _service = service;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] TokenRequest tokenRequest)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenRequest.Token);
            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    FullName = payload.Name,
                    Avatar = payload.Picture
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return BadRequest(createResult.Errors);
                }

                await _userManager.AddToRoleAsync(user, "Customer");
            }

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

            var user = await _userManager.FindByEmailAsync(payload.Email!);

            if (user == null)
            {
                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    FullName = payload.Name,
                    Avatar = payload.Picture!.Data!.Url
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return BadRequest(createResult.Errors); 
                }
                await _userManager.AddToRoleAsync(user, "Customer");
                
            }

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
            if (registerDto.UserName.Length < 6)
                return BadRequest("UserName không được dưới 6 ký tự");
            if (await CheckUsernameExistsAsync(registerDto.UserName))
                return BadRequest("Username đã tồn tại");
            if (await EmailExists(registerDto.Email))
            {
                return BadRequest("Email đã tồn tại");
            }
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
        [HttpGet("userId/{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userDto = UserMapper.EntityToUserDto(user);
            userDto.Token = await _tokenService.CreateToken(user);
            return Ok(userDto);
        }
        [HttpPost("Add/User")]
        public async Task<ActionResult<UserDto>> CreateUser([FromForm] UserAdd userAdd)
        {
            if (!IsPasswordValid(userAdd.Password))
                return BadRequest("Mật khẩu phải dài ít nhất 6 ký tự, bao gồm một chữ hoa, một chữ thường, một số và một ký tự đặc biệt.");
            if (userAdd.UserName.Length < 6)
                return BadRequest("UserName không được dưới 6 ký tự");
            if (await CheckUsernameExistsAsync(userAdd.UserName))
                return BadRequest("Username đã tồn tại");
            if (await EmailExists(userAdd.Email))
            {
                return BadRequest("Email đã tồn tại");
            }
            var user = new AppUser
            {
                FullName = userAdd.FullName,
                Email = userAdd.Email,
                UserName = userAdd.UserName,

            };
            if (userAdd.Avatar?.Length > 0)
            {
                var resultUpload = await _cloudinaryService.UploadImageAsync(userAdd.Avatar);
                user.Avatar = resultUpload.Url;
            }
            else
            {
                user.Avatar = "user.jpg";
            }

            var result = await _userManager.CreateAsync(user, userAdd.Password);

            if (!result.Succeeded)
                return BadRequest("Không thể tạo tài khoản");

            var roleResult = await _userManager.AddToRoleAsync(user, userAdd.Role);
            if (!roleResult.Succeeded)
                return BadRequest("Không thể gán quyền cho tài khoản");

            var userDto = UserMapper.EntityToUserDto(user);
            userDto.Token = await _tokenService.CreateToken(user);

            return Ok(userDto);
        }
        [HttpPut("Update/User")]
        public async Task<ActionResult<UserDto>> UpdateUser([FromForm] UserUpdate userDto)
        {
            if (userDto == null)
                return BadRequest("Dữ liệu không hợp lệ");

            if (await EmailExists(userDto.Email, userDto.Id))
            {
                return BadRequest("Email đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null)
                return NotFound("User không tồn tại");

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                if (!IsPasswordValid(userDto.Password))
                    return BadRequest("Mật khẩu phải dài ít nhất 6 ký tự, bao gồm một chữ hoa, một chữ thường, một số và một ký tự đặc biệt.");

                var passwordHash = _userManager.PasswordHasher.HashPassword(user, userDto.Password);
                user.PasswordHash = passwordHash;
            }
            user.UserName = userDto.UserName ?? user.UserName;
            user.Email = userDto.Email ?? user.Email;
            user.FullName = userDto.FullName ?? user.FullName;

            if (userDto.Avatar?.Length > 0)
            {
                var resultUpload = await _cloudinaryService.UploadImageAsync(userDto.Avatar);
                user.Avatar = resultUpload.Url;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest("Cập nhật không thành công");
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Kiểm tra nếu Role mới bằng null hoặc rỗng
            string? roleToUpdate = string.IsNullOrEmpty(userDto.Role)
                ? currentRoles.FirstOrDefault()
                : userDto.Role;
            if (!string.IsNullOrEmpty(roleToUpdate))
            {
                if (currentRoles.Count > 0)
                {
                    var currentRole = currentRoles.FirstOrDefault();

                    if (currentRole != roleToUpdate)
                    {
                        var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, currentRole);
                        if (!removeRoleResult.Succeeded)
                            return BadRequest("Không thể xóa quyền cũ của tài khoản");
                        var addRoleResult = await _userManager.AddToRoleAsync(user, roleToUpdate);
                        if (!addRoleResult.Succeeded)
                            return BadRequest("Không thể gán quyền mới cho tài khoản");
                    }
                }
                else
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(user, roleToUpdate);
                    if (!addRoleResult.Succeeded)
                        return BadRequest("Không thể gán quyền cho tài khoản");
                }
            }

            var updatedUserDto = UserMapper.EntityToUserDto(user);
            updatedUserDto.Token = await _tokenService.CreateToken(user);

            return Ok(updatedUserDto);
        }


        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers([FromQuery] UserParams userParams)
        {
            var customerClaimIds = await _service.GetUsersWithoutClaimAsync(ClaimStore.Message_Reply);
            var query = _userManager.Users.AsNoTracking()
                .Where(user => customerClaimIds.Contains(user.Id));

            if (!string.IsNullOrEmpty(userParams.Search))
            {
                query = query.Where(u => u.FullName.ToLower().Contains(userParams.Search.ToLower())
                    || u.Email.ToLower().Contains(userParams.Search.ToLower())
                    || u.UserName.ToLower().Contains(userParams.Search.ToLower()));
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                query = userParams.OrderBy switch
                {
                    "username" => query.OrderBy(u => u.UserName),
                    "username_desc" => query.OrderByDescending(u => u.UserName),
                    "email" => query.OrderBy(u => u.Email),
                    "email_desc" => query.OrderByDescending(u => u.Email),
                    _ => query.OrderBy(u => u.UserName)
                };
            }

            var count = await query.CountAsync();

            var users = await query
               .Skip((userParams.PageNumber - 1) * userParams.PageSize)
               .Take(userParams.PageSize)
               .ToListAsync();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {

                var userDto = UserMapper.EntityToUserDto(user);
                if (user.LockoutEnd.HasValue && (user.LockoutEnd > DateTimeOffset.UtcNow || user.LockoutEnd == DateTimeOffset.MaxValue))
                {
                    userDto.IsLocked = true;
                }
                else
                {
                    userDto.IsLocked = false;
                }
                userDto.Token = await _tokenService.CreateToken(user);
                userDtos.Add(userDto);
            }

            var pagedList = new PagedList<UserDto>(userDtos, count, userParams.PageNumber, userParams.PageSize);

            Response.AddPaginationHeader(pagedList);

            return Ok(pagedList);
        }
        [HttpPut("Lock/User")]
        public async Task<ActionResult> LockUser(string id, [FromQuery] int? minutes, [FromQuery] int? hours, [FromQuery] int? days)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User không tồn tại");

            DateTimeOffset lockoutEnd;
            if (minutes.HasValue) lockoutEnd = DateTimeOffset.Now.AddMinutes(minutes.Value);
            else if (hours.HasValue) lockoutEnd = DateTimeOffset.Now.AddHours(hours.Value);
            else if (days.HasValue) lockoutEnd = DateTimeOffset.Now.AddDays(days.Value);
            else lockoutEnd = DateTimeOffset.MaxValue;

            user.LockoutEnd = lockoutEnd;

            if (user.LockoutEnd <= DateTimeOffset.UtcNow)
            {
                user.LockoutEnd = null;
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest("Lock user thất bại");

            return Ok(new { message = "Lock user thành công" });
        }
        [HttpPut("Unlock/User")]
        public async Task<ActionResult> UnlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User không tồn tại");

            user.LockoutEnd = null;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest("Unlock user thất bại");

            return Ok(new { message = "Unlock user thành công" });
        }

        private async Task<bool> CheckUsernameExistsAsync(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }
        [HttpGet("email-exists")]
        private async Task<bool> EmailExists(string email, string userId = null)
        {
            var existingUser = await _userManager.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            if (existingUser == null || existingUser.Id == userId)
                return false;
            return true;
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
            return password.Length >= 6 &&
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsLower) &&
                    password.Any(char.IsDigit) &&
                    password.Any(char.IsPunctuation);
        }
    }
}
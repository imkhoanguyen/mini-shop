
using API.Extensions;
using API.Helper;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs.Auth.Permissions;
using Shop.Application.Ultilities;
using Shop.Infrastructure.DataAccess;
using System.Security.Claims;

namespace API.Controllers
{
    public class RoleController : BaseApiController
    {
        //private readonly RoleManager<AppRole> _roleManager;
        //private readonly StoreContext _context;

        //public RoleController(RoleManager<AppRole> roleManager, StoreContext context)
        //{
        //    _roleManager = roleManager;
        //    _context = context;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetRoles([FromQuery] RoleParams roleParams)
        //{
        //    var query = _roleManager.Roles.AsQueryable();

        //    if (!string.IsNullOrEmpty(roleParams.Query))
        //    {
        //        query = query.Where(r => r.Name!.ToLower().Contains(roleParams.Query.ToLower()));
        //    }

        //    if (!string.IsNullOrEmpty(roleParams.OrderBy))
        //    {
        //        query = roleParams.OrderBy switch
        //        {
        //            "id" => query.OrderBy(r => r.Id),
        //            "id_desc" => query.OrderByDescending(r => r.Id),
        //            "name" => query.OrderBy(r => r.Name!.ToLower()),
        //            "name_desc" => query.OrderByDescending(r => r.Name!.ToLower()),
        //            "created_desc" => query.OrderByDescending(r => r.Created),
        //            "created" => query.OrderBy(r=>r.Created),
        //            _ => query.OrderByDescending(r => r.Created),
        //        };
        //    }

        //    // Không dùng RoleDto.FromEntity ở đây vì phương thức này ko thể dịch sang sql
        //    var roleDtosQuery = query.Select(r => new RoleDto // mapping
        //    {
        //        Id = r.Id,
        //        Name = r.Name!,
        //        Description = r.Description!,
        //        Created = r.Created,
        //    });

        //    var roleDtos = await PagedList<RoleDto>.CreateAsync(roleDtosQuery, roleParams.PageNumber, roleParams.PageSize);

        //    Response.AddPaginationHeader(roleDtos);

        //    return Ok(roleDtos);
        //}


        //[HttpGet("{id}")]
        //public async Task<ActionResult<AppRole>> GetRole(string id)
        //{
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null) return NotFound();

        //    var roleDto = RoleDto.FromEntity(role); // map

        //    return Ok(roleDto);
        //}

        //[HttpPost]
        //[Authorize(Policy = ClaimStore.Role_Create)]
        //public async Task<ActionResult<RoleDto>> CreateRole(RoleCreateDto roleCreateDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);


        //    if (await RoleNameExists(roleCreateDto.Name))
        //        return BadRequest("Tên quyền đã tồn tại");

        //    var appRole = new AppRole
        //    {
        //        Name = roleCreateDto.Name,
        //        Description = roleCreateDto.Description,
        //    };

        //    var result = await _roleManager.CreateAsync(appRole);

        //    if (result.Succeeded)
        //    {
        //        var roleDto = RoleDto.FromEntity(appRole);
        //        return CreatedAtAction("GetRole", new { id = appRole.Id }, roleDto);
        //    }

        //    return BadRequest("Đã xảy ra lỗi khi thêm quyền");
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateRole(string id, RoleDto roleDto)
        //{
        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (roleDto.Id != id)
        //        return BadRequest("Không thể cập nhật quyền");

        //    if (await CheckEdit(roleDto.Name, id))
        //        return BadRequest("Tên quyền đã tồn tại");

        //    var roleFromDb = await _roleManager.FindByIdAsync(id);

        //    if (roleFromDb == null) return NotFound("Không tìm thấy quyền");

        //    roleFromDb.Name = roleDto.Name;
        //    roleFromDb.Description = roleDto.Description;

        //    var result = await _roleManager.UpdateAsync(roleFromDb);

        //    if (result.Succeeded)
        //        return NoContent();

        //    return BadRequest("Đã xảy ra lỗi khi cập nhật quyền");
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteRole(string id)
        //{
        //    // check thêm nếu không role đang được gán cho 1 user bất kỳ thì ko đc xóa


        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null) return NotFound();

        //    var result = await _roleManager.DeleteAsync(role);
        //    if (result.Succeeded)
        //        return NoContent();
        //    return BadRequest("Đã xảy ra lỗi khi xóa role");
        //}

        //[HttpGet("permissions")]
        //public ActionResult<List<PermissionGroupDto>> GetAllPermission()
        //{
        //    return ClaimStore.AllPermissionGroups;
        //}

        //[HttpGet("claims/{roleId}")]
        //public async Task<ActionResult<List<string>>> GetRoleClaims(string roleId)
        //{
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role == null) return NotFound("Không tìm thấy quyền");

        //    var claims = await _roleManager.GetClaimsAsync(role);

        //    return Ok(claims.Select(c => c.Value).ToList());
        //}

        //[HttpPut("update-claims/{roleId}")]
        //public async Task<IActionResult> UpdateRoleClaims(string roleId, [FromBody] List<string> newRoleClaims)
        //{
        //    var role = await _roleManager.FindByIdAsync(roleId);
        //    if (role == null) return NotFound("Không tìm thấy quyền");

        //    var currentClaims = await _roleManager.GetClaimsAsync(role);
        //    var currentClaimValues = currentClaims.Select(c => c.Value).ToList();


        //    var claimsToAdd = newRoleClaims.Except(currentClaimValues).ToList();

        //    var claimsToRemove = currentClaimValues.Except(newRoleClaims).ToList();

        //    // Xóa các claim không còn trong danh sách mới
        //    foreach (var claimValue in claimsToRemove)
        //    {
        //        var claim = currentClaims.FirstOrDefault(c => c.Value == claimValue);
        //        if (claim != null)
        //        {
        //            var result = await _roleManager.RemoveClaimAsync(role, claim);
        //            if (!result.Succeeded) return BadRequest($"Không thể xóa claim: {claimValue}");
        //        }
        //    }

        //    // Thêm các claim mới
        //    foreach (var claimValue in claimsToAdd)
        //    {
        //        var result = await _roleManager.AddClaimAsync(role, new Claim("Permission", claimValue));
        //        if (!result.Succeeded) return BadRequest($"Không thể thêm claim: {claimValue}");
        //    }

        //    return Ok(new { message = "Cập nhật chức năng của quyền thành công" });
        //}


        //private async Task<bool> RoleIdExists(string id)
        //{
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role != null) return true;
        //    return false;
        //}

        //private async Task<bool> RoleNameExists(string roleName)
        //{
        //    var role = await _roleManager.Roles
        //        .FirstOrDefaultAsync(r => r.Name!.ToLower() == roleName.ToLower());
        //    if (role != null) return true;
        //    return false;
        //}

        //private async Task<bool> CheckEdit(string roleName, string roleId)
        //{
        //    var role = await _roleManager.Roles
        //        .FirstOrDefaultAsync(r => r.Name!.ToLower() == roleName.ToLower() && r.Id != roleId);
        //    if (role != null) return true;
        //    return false;
        //}


    }
}

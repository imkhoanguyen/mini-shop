using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppRole>>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDtos = roles.Select(RoleDto.FromEntity);
            return Ok(roleDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppRole>> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var roleDto = RoleDto.FromEntity(role);

            return Ok(roleDto);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole(RoleCreateDto roleCreateDto)
        {
            // check validation ...  (lam sau)


            if (await RoleNameExists(roleCreateDto.Name))
                return BadRequest("Role đã tồn tại");

            var appRole = new AppRole
            {
                Name = roleCreateDto.Name,
                Description = roleCreateDto.Description,
            };

            var result = await _roleManager.CreateAsync(appRole);

            if (result.Succeeded)
            {
                var roleDto = RoleDto.FromEntity(appRole);
                return CreatedAtAction("GetRole", new { id = appRole.Id }, roleDto);
            }

            return BadRequest("Đã xảy ra lỗi khi thêm role");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(string id, RoleDto roleDto)
        {
            if (roleDto.Id != id || await RoleNameExists(roleDto.Name)
                || await RoleIdExists(roleDto.Id))
                return BadRequest("Không thể cập nhật role");

            var appRole = new AppRole
            {
                Id = id,
                Name = roleDto.Name,
                Description = roleDto.Description,
            };

            var result = await _roleManager.UpdateAsync(appRole);

            if (result.Succeeded)
                return NoContent();

            return BadRequest("Đã xảy ra lỗi khi cập nhật role");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            // check thêm nếu không role đang được gán cho 1 user bất kỳ thì ko đc xóa


            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return NoContent();
            return BadRequest("Đã xảy ra lỗi khi xóa role");
        }

        private async Task<bool> RoleIdExists(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null) return true;
            return false;
        }

        private async Task<bool> RoleNameExists(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null) return true;
            return false;
        }
    }
}

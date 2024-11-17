using Application.Services;
using Core.DTOS.Product;
using Core.DTOS.Role;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    public class RolesController:BaseAdminController
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRolls() { 
            return Ok(await roleService.GetRolesAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRole(int id)
        {
            return Ok(await roleService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreatProduct(AddRoleDto addRoleDto)
        {
            var role = await roleService.AddRole(addRoleDto.RoleName);

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }
    }
}

using Application.Services;
using Core.DTOS.Product;
using Core.DTOS.Shared;
using Core.DTOS.User;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    public class UsersController:BaseAdminController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListWithCountDto<UserDto>>>> GetAllUsers([FromQuery] UserQueryDto userQueryDto)
        {

            return Ok(await usersService.GetAndCountAll(userQueryDto));
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {

            return Ok(await usersService.GetUserByIdAsync(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            await usersService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("addRole")]
        public async Task<ActionResult> AddUserToRole(MangeUserRoleDto mangeUserRoleDto)
        {
            await usersService.AssignRoleTpUser(mangeUserRoleDto);
            return NoContent();
        }

        [HttpPost("removeRole")]
        public async Task<ActionResult> RemoveUserRole(MangeUserRoleDto mangeUserRoleDto)
        {
            await usersService.UnAssignRoleTpUser(mangeUserRoleDto);
            return NoContent();
        }
    }
}

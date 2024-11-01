using API.Extensions;
using Core.Dtos.User;
using Core.Errors;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }


        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserDto>> GetCurrentUsert()
        {

            var userId= HttpContext.User.GetUserId();

            if(userId == 0)
                throw new UserUnauthorizedException();
            
            var user = await usersService.GetUserByIdAsync(userId);

            var roles = HttpContext.User.GetUserRoles();

            return Ok(new UserDto
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles,
            }); ;
            
        }

    }
}

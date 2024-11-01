using Core.Dtos.Authentication;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService
        )
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<AuthenticationDto> Login(LoaginDto loaginDto)
        {
            var user = await userManager.FindByEmailAsync( loaginDto.Email );

            if (user == null || !await userManager.CheckPasswordAsync(user , loaginDto.Password))
                throw new UserUnauthorizedException("Invalid Credentials");

            var roles = await userManager.GetRolesAsync(user);

            var token = await tokenService.GenerateToken(user , roles);



            return new AuthenticationDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = token,
                Roles = roles
            };
        }

        public async Task<AuthenticationDto> Register(RegisterDto registerDto)
        {
            if (await userManager.FindByEmailAsync(registerDto.Email) is not null)
                throw new BadRequsetException(err:new[]{ "Email is already exsist" } );

            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                throw new BadRequsetException(err: errors);
            }

            await userManager.AddToRoleAsync(user, RoleEnum.Customer.ToString());

            var token = await tokenService.GenerateToken(user);

            return new AuthenticationDto
            {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                Token = token
            };
        }
    }
}

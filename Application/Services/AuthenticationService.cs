using AutoMapper;
using Core.DTOS.Authentication;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IMapper mapper
,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }

        public async Task<AuthenticationDto> Login(LoaginDto loaginDto)
        {
            var user = await userManager.FindByEmailAsync(loaginDto.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, loaginDto.Password))
                throw new UserUnauthorizedException("Invalid Credentials");


            var roles = await userManager.GetRolesAsync(user);

            var token = await tokenService.GenerateToken(user , roles);

            // Use AutoMapper to map User to AuthenticationDto
            var authenticationDto = mapper.Map<AuthenticationDto>(user);

            // Set token and roles
            authenticationDto.Token = token;
            authenticationDto.Roles = roles;
            authenticationDto.UserId = user.Id;
            return authenticationDto;
        }

   
        public async Task<AuthenticationDto> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                throw new BadRequestException(err: errors);
            }

            await userManager.AddToRoleAsync(user, UserRole.Customer.ToString());

            var token = await tokenService.GenerateToken(user);

            // Use AutoMapper to map User to AuthenticationDto
            var authenticationDto = mapper.Map<AuthenticationDto>(user);

            // Set token and roles
            authenticationDto.Token = token;
            authenticationDto.UserId = user.Id;

            return authenticationDto;

        }
    }
}

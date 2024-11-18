using AutoMapper;
using Core.DTOS.Authentication;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Stripe.Forwarding;

namespace Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.emailSender = emailSender;
        }

        public async Task CofirmationEmail(ConfirmationEmailDto confirmationEmailDto)
        {
            var user = await userManager.FindByIdAsync(confirmationEmailDto.UserId.ToString());

            if (user == null)
                throw new NotFoundException("User not found");

            var result = await userManager.ConfirmEmailAsync(user, confirmationEmailDto.Token);

            Console.WriteLine(result.Succeeded);
            
            ResultCheck(result);

            return;
        }

        public async Task<ConfirmationEmailResponseDto> CreateConfiramtionToken(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                throw new NotFoundException("User dont exsist");

            if (await userManager.IsEmailConfirmedAsync(user))
                throw new BadRequestException("Email is already confirmed.");

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = $"{configuration["ApiUrl"]}/api/authentication/confirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            await emailSender.SendEmailAsync(
                email, 
                "Confirm Your Email",
                $"Please confirm your email by clicking the link: <a href='{confirmationLink}'>Confirm Email</a>"
            );


            return new ConfirmationEmailResponseDto
            {
                Message = "Please check your email to confirm your account.",
                IsEmailConfirmationRequired = true,
            };

        }

        public async Task<AuthenticationDto> Login(LoaginDto loaginDto)
        {
            var user = await userManager.FindByEmailAsync(loaginDto.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, loaginDto.Password))
                throw new UserUnauthorizedException("Invalid Credentials");

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                throw new BadRequestException("Email is not confirmed. Please confirm your email first.");
            }

            var roles = await userManager.GetRolesAsync(user);

            var token = await tokenService.GenerateToken(user , roles);

            var authenticationDto = mapper.Map<AuthenticationDto>(user);

            // Set token and roles
            authenticationDto.Token = token;
            authenticationDto.Roles = roles;
            authenticationDto.UserId = user.Id;
            return authenticationDto;
        }

   
        public async Task<ConfirmationEmailResponseDto> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            ResultCheck(result);

            await userManager.AddToRoleAsync(user, UserRole.Customer.ToString());

            return await CreateConfiramtionToken(registerDto.Email);

        }

        private void ResultCheck(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                throw new BadRequestException(err: errors);
            }
        }
    }
}

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Strategies;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Strategies;

namespace API.Extensions
{
    public static class CustomServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services) {

            services.AddTransient(typeof(IBaseService<>), typeof(BaseSerivce<>));
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICartService , CartService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();    
            services.AddScoped<IUsersService, UsersService>();    
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IOrdersService, OrderaService>();
            services.AddScoped<IPaymentsService , PaymentsService>();
            services.AddScoped<IStripeService , StripeService>();
            services.AddScoped<IBrandsService , BrandsService>();
            //services.AddScoped<IPaymentMethodStrategy , StripePaymentStrategy>();
            services.AddScoped<IMediaStorageService, LocalStorageService>();
            services.AddScoped<PaymentFactory>();

            return services;
        }
    }
}

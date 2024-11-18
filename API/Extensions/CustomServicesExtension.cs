using Application.Factories;
using Application.Services;
using Application.Strategies;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace API.Extensions
{
    public static class CustomServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork , UntitOfWork>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICategoriesService , CategoriesService>();
            services.AddScoped<IBrandsService , BrandsServices>();
            services.AddScoped<IProductsService , ProductsService>();
            services.AddScoped<IPaymentMethodService , PaymentMethodService>();
            services.AddScoped<ICartService , CartService>();
            services.AddScoped<IMediaStorageService, LocalStorageService>();
            services.AddScoped<IPaymentsService, PaymentsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<PaymentFactory>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IEmailSender , GmailEmailSender>();
            services.AddScoped<CashOnDeliveryPaymentStrategy>();
            services.AddScoped<StripePaymentStrategy>();
            return services;
        }
    }
}

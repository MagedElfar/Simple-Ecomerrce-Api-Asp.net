using Application.Factories;
using Application.Services;
using Application.Strategies;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Repositories;

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
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<PaymentFactory>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CashOnDeliveryPaymentStrategy>();
            services.AddScoped<StripePaymentStrategy>();
            return services;
        }
    }
}

using Core.Options;

namespace API.Extensions
{
    public static class OptionsExtensions
    {

        public static IServiceCollection AddApplicationOptions(this IServiceCollection services , WebApplicationBuilder builder) {

            services.Configure<JWTOption>(builder.Configuration.GetSection("JWT"));
            services.Configure<StripeOption>(builder.Configuration.GetSection("StripeSettings"));

            return services;
        }
    }
}

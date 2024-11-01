using Core.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = acctionContext =>
                {
                    var errors = acctionContext.ModelState
                    .Where(e => e.Value?.Errors?.Count > 0)
                    .SelectMany(
                        e => e.Value?.Errors,
                       (e, msg) => $"{e.Key}: {msg.ErrorMessage}"

                    )
                    //.Select(
                    //    e => e.ErrorMessage,
                    //)
                    .ToArray();

                    var errorsResponse = new ApiValidationErrorResponse { Errors = errors };

                    return new BadRequestObjectResult(errorsResponse);

                };

            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin() // or use .WithOrigins("https://example.com") to specify specific origins
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            return services;

        }
    }
}

using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;

        }

        public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app)
        {
           app.UseSwagger();
           app.UseSwaggerUI();
            return app;
        }
    }
}

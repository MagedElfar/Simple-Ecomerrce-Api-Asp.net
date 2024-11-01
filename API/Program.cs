using API.Extensions;
using API.Middlewares;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Stripe;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDatabaseConnections(builder);
            builder.Services.AddApplicationOptions(builder);
            builder.Services.AddSwaggerServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddDatabasRepositories();
            builder.Services.AddApplicationAddAuthentication(builder);
            builder.Services.AddCustomServices();
           
            var app = builder.Build();

            await app.MigrateAndSeedDatabaseAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

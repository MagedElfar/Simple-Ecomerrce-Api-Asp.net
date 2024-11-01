using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class DatabaseExtensions
    {

        public static IServiceCollection AddDatabaseConnections(this IServiceCollection services , WebApplicationBuilder builder) {

            services.AddDbContext<AdbContext>(opthions =>
            {
                opthions.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            services
               .AddIdentity<ApplicationUser, ApplicationRole>(options =>
               {
                   options.Password.RequireDigit = false;            // Require at least one digit (can disable)
                   options.Password.RequireLowercase = false;        // Require at least one lowercase character (can disable)
                   options.Password.RequireUppercase = false;        // Require at least one uppercase character (can disable)
                   options.Password.RequireNonAlphanumeric = false;
               })
               .AddSignInManager<SignInManager<ApplicationUser>>()
               .AddEntityFrameworkStores<AdbContext>()
               .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configretion = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);

                configretion.AbortOnConnectFail = true;

                return ConnectionMultiplexer.Connect(configretion);
            });


            return services;
        }

        public static IServiceCollection AddDatabasRepositories(this IServiceCollection services) {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UntitOfWork>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();

            return services;
        }
        public static async Task MigrateAndSeedDatabaseAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AdbContext>();
                var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManger = services.GetRequiredService<RoleManager<ApplicationRole>>();

                try
                {
                    // Apply migrations
                    await context.Database.MigrateAsync();

                    // Seed the database
                    await AdbContextSeed.SeedAsync(context);
                    await AdbContextSeed.SeedRolsAsync(roleManger);
                    await AdbContextSeed.SeedUsersAsync(userManger);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during migration or seeding.");
                }
            }
        }
    }
}

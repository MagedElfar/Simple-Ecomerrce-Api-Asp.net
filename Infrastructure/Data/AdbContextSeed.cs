using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AdbContextSeed
    {
        public static async Task SeedAsync(AdbContext context)
        {
            if (!context.PaymentMethods.Any())
            {
                var paymentMethods = new List<PaymentMethod>
                {
                    new PaymentMethod{Name = "Cash on Delivery"},
                    new PaymentMethod{Name = "Credit and Debit Cards"},
                };

                context.AddRange(paymentMethods);

                await context.SaveChangesAsync();
            }

        }


        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "maged92",
                    Email = "maged.1992.me@gmail.com"
                };

                await userManager.CreateAsync(user, "12345678");

                await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
            }
        }

        public static async Task SeedRolsAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<ApplicationRole>
                {
                    new ApplicationRole
                    {
                        Name = UserRole.Customer.ToString()
                    },
                    new ApplicationRole
                    {
                        Name = UserRole.Admin.ToString()
                    }
                };

                foreach (var role in roles)
                {
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

            }
        }

    }
}

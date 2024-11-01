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
          
            if (!context.Brands.Any()) { 
                var brands = new List<Brand>
                {
                    new Brand{Name = "Nike"},
                    new Brand{Name = "Zara"},
                    new Brand{Name = "H&M"},
                };

                context.AddRange(brands);

                await context.SaveChangesAsync();
            }

            if (!context.ProductTypes.Any())
            {
                var types = new List<ProductType>
                {
                    new ProductType{Name = "Boards"},
                    new ProductType{Name = "Hats"},
                    new ProductType{Name = "Boots"},
                    new ProductType{Name = "Gloves"},
                };

                context.AddRange(types);

                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product{Name = "HUAWEI MatePad 12 X" , SKU = "lp-001" , Description = "laptop" , Price = 30_000 , BrandId = null , ProductTypeId = null},
                    new Product{Name = "Baseball cap" , SKU = "cap-001" , Description = "Baseball cap with embroidery Carpathian" , Price = 123.99m , BrandId = 1 , ProductTypeId = 2},
                };

                context.AddRange(products);

                await context.SaveChangesAsync();
            }

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

               await userManager.CreateAsync(user , "12345678");

                await userManager.AddToRoleAsync(user, RoleEnum.Admin.ToString());
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
                        Name = RoleEnum.Customer.ToString()
                    },
                    new ApplicationRole
                    {
                        Name = RoleEnum.Admin.ToString()
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

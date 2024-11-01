using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AdbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AdbContext(DbContextOptions options) :base(options)
        {
            
        }

        public DbSet<ApplicationUser> Users {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems  { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<CartItem>();
            //modelBuilder.Ignore<ShippingAddress>();
            modelBuilder.Ignore<ShippingMethod>();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

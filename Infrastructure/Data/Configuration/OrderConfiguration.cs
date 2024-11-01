using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress,
            sa => {
                sa.Ignore(x => x.Id);
                sa.Property(x => x.FirstName).HasColumnName("FirstName");
                sa.Property(x => x.LastName).HasColumnName("LastName");
                sa.Property(x => x.Street).HasColumnName("Street");
                sa.Property(x => x.City).HasColumnName("City");
                sa.Property(x => x.State).HasColumnName("State");
                sa.Property(x => x.Zipcode).HasColumnName("Zipcode");
                sa.Property(x => x.Phone).HasColumnName("Phone");
            });


            builder.Property(x => x.PaymentMethodId)
                .IsRequired(false);

            //builder.HasMany(x => x.products)
            //    .WithMany(x => x.Orders)
            //    .UsingEntity<OrderItem>();

            builder.HasMany(x => x.Products)
            .WithMany(x => x.Orders)
            .UsingEntity<OrderItem>(
                j => j
                    .HasOne(orderItem => orderItem.Product)
                    .WithMany()
                    .HasForeignKey(orderItem => orderItem.ProductId)
                    .OnDelete(DeleteBehavior.Restrict),  // Restrict delete on Products side
                j => j
                    .HasOne(orderItem => orderItem.Order)
                    .WithMany(x => x.Items)
                    .HasForeignKey(orderItem => orderItem.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)   // Optionally, set delete behavior for Orders
            );


            builder.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(x => x.PaymentMethod)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.OrderStatus)
           .HasConversion(
               x => x.ToString(),
               x => (OrderStatusEnum) Enum.Parse(typeof(OrderStatusEnum), x)
            )
           .HasColumnType("VARCHAR")
           .HasMaxLength(100);
        }
    }
}

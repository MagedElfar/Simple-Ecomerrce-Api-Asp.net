using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.UseTpcMappingStrategy();
            //builder.HasOne(x => x.User)
            // .WithOne(x => x.Address)
            // .HasForeignKey<Address>(x => x.UserId);
        }
    }
}

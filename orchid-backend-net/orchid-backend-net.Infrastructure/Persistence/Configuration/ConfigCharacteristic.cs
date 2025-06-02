using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigCharacteristic : IEntityTypeConfiguration<Characteristic>
    {
        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.HasOne(x => x.SeedlingAttributeID)
                .WithMany()
                .HasForeignKey(x => x.SeedlingAttribute)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SeedlingID)
                .WithMany()
                .HasForeignKey(x => x.Seedling)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

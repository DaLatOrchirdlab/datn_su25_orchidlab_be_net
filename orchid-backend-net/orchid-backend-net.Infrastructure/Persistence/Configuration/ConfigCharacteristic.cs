﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigCharacteristic : IEntityTypeConfiguration<Characteristic>
    {
        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder.HasOne(x => x.SeedlingAttribute)
                .WithMany()
                .HasForeignKey(x => x.SeedlingAttributeID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Seedling)
                .WithMany()
                .HasForeignKey(x => x.SeedlingID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

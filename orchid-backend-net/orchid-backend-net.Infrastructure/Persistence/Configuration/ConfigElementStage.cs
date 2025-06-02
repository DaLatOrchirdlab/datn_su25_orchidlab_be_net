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
    internal class ConfigElementStage : IEntityTypeConfiguration<ElementInStage>
    {
        public void Configure(EntityTypeBuilder<ElementInStage> builder)
        {
            builder.HasOne(x => x.ElementID)
                .WithMany()
                .HasForeignKey(x => x.Element)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.StageID)
                .WithMany()
                .HasForeignKey(x => x.Stage)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

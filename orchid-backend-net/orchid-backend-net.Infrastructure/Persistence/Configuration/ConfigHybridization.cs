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
    internal class ConfigHybridization : IEntityTypeConfiguration<Hybridization>
    {
        public void Configure(EntityTypeBuilder<Hybridization> builder)
        {
            builder.HasOne(x => x.ParentID)
                .WithMany()
                .HasForeignKey(x => x.Parent)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ExperimentLogID)
                .WithMany()
                .HasForeignKey(x => x.ExperimentLog)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

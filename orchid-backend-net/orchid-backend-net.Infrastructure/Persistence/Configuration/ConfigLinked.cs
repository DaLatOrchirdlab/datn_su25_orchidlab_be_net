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
    internal class ConfigLinked : IEntityTypeConfiguration<Linked>
    {
        public void Configure(EntityTypeBuilder<Linked> builder)
        {
            builder.HasOne(x => x.SampleID)
                .WithMany()
                .HasForeignKey(x => x.Sample)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ExperimentLogID)
                .WithMany()
                .HasForeignKey(x => x.ExperimentLog)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TaskID)
                .WithMany()
                .HasForeignKey(x => x.Task)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

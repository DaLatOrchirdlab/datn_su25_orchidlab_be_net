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
    internal class ConfigExperimentLog : IEntityTypeConfiguration<ExperimentLog>
    {
        public void Configure(EntityTypeBuilder<ExperimentLog> builder)
        {
            builder.HasOne(x => x.TissueCultureBatchID)
                .WithMany()
                .HasForeignKey(x => x.TissueCultureBatch)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.MethodID)
                .WithMany()
                .HasForeignKey(x => x.Method)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

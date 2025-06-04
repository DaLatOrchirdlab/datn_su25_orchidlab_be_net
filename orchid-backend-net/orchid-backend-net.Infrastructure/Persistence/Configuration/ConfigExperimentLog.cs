using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigExperimentLog : IEntityTypeConfiguration<ExperimentLog>
    {
        public void Configure(EntityTypeBuilder<ExperimentLog> builder)
        {
            builder.HasOne(x => x.TissueCultureBatch)
                .WithMany()
                .HasForeignKey(x => x.TissueCultureBatchID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Method)
                .WithMany()
                .HasForeignKey(x => x.MethodID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

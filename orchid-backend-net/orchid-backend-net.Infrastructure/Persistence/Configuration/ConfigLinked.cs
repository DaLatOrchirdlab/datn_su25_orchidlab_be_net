using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigLinked : IEntityTypeConfiguration<Linkeds>
    {
        public void Configure(EntityTypeBuilder<Linkeds> builder)
        {
            builder.HasOne(x => x.Sample)
                .WithMany()
                .HasForeignKey(x => x.SampleID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ExperimentLog)
                .WithMany()
                .HasForeignKey(x => x.ExperimentLogID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

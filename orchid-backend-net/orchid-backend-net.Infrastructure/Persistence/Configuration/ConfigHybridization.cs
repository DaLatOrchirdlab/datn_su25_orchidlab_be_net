using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigHybridization : IEntityTypeConfiguration<Hybridization>
    {
        public void Configure(EntityTypeBuilder<Hybridization> builder)
        {
            builder.HasOne(x => x.Parent)
                .WithMany()
                .HasForeignKey(x => x.ParentID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ExperimentLog)
                .WithMany()
                .HasForeignKey(x => x.ExperimentLogID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

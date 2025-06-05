using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigStage : IEntityTypeConfiguration<Stage>
    {
        public void Configure(EntityTypeBuilder<Stage> builder)
        {
            builder.HasOne(x => x.Method)
                .WithMany()
                .HasForeignKey(x => x.MethodID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

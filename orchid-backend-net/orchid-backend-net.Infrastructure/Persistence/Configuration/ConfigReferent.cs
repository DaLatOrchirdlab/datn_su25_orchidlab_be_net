using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigReferent : IEntityTypeConfiguration<Referent>
    {
        public void Configure(EntityTypeBuilder<Referent> builder)
        {
            builder.HasOne(x => x.Stage)
                .WithMany()
                .HasForeignKey(x => x.StageID);
            builder.HasOne(x => x.StageAttribute)
                .WithMany()
                .HasForeignKey(x => x.StageAttributeID);
        }
    }
}

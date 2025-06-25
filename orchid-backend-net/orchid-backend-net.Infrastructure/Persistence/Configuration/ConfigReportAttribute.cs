using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigReportAttribute : IEntityTypeConfiguration<ReportAttributes>
    {
        public void Configure(EntityTypeBuilder<ReportAttributes> builder)
        {
            builder.HasMany(x => x.Referents)
                .WithMany(x => x.ReportAttributes);

            builder.HasOne(x => x.Report)
                .WithMany()
                .HasForeignKey(x => x.ReportID);
        }
    }
}

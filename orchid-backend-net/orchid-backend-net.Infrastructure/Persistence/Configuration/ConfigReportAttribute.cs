using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigReportAttribute : IEntityTypeConfiguration<ReportAttribute>
    {
        public void Configure(EntityTypeBuilder<ReportAttribute> builder)
        {
            builder.HasOne(x => x.ReferentData)
                .WithMany()
                .HasForeignKey(x => x.ReferentDataID);
            builder.HasOne(x => x.Report)
                .WithMany()
                .HasForeignKey(x => x.ReportID);
        }
    }
}

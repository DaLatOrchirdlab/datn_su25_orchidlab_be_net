using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;


namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigImg : IEntityTypeConfiguration<Img>
    {
        public void Configure(EntityTypeBuilder<Img> builder)
        {
            builder.HasOne(x => x.Report)
                .WithMany()
                .HasForeignKey(x => x.ReportID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

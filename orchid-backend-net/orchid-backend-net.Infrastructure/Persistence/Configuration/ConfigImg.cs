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
    internal class ConfigImg : IEntityTypeConfiguration<Img>
    {
        public void Configure(EntityTypeBuilder<Img> builder)
        {
            builder.HasOne(x => x.ReportID)
                .WithMany()
                .HasForeignKey(x => x.Report)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

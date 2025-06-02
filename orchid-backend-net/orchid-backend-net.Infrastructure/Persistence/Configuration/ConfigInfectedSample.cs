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
    internal class ConfigInfectedSample : IEntityTypeConfiguration<InfectedSample>
    {
        public void Configure(EntityTypeBuilder<InfectedSample> builder)
        {
            builder.HasOne(x => x.Sample)
                .WithMany()
                .HasForeignKey(x => x.Sample)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.DiseaseID)
                .WithMany()
                .HasForeignKey(x => x.Disease)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

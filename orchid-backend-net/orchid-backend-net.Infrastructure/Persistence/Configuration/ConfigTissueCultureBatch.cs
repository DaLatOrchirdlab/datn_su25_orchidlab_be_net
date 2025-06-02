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
    internal class ConfigTissueCultureBatch : IEntityTypeConfiguration<TissueCultureBatch>
    {
        public void Configure(EntityTypeBuilder<TissueCultureBatch> builder)
        {
            builder.HasOne(x => x.LabRoomID)
                .WithMany()
                .HasForeignKey(x => x.LabRoom)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

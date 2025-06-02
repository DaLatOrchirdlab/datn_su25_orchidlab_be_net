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
    internal class ConfigReferent : IEntityTypeConfiguration<Referent>
    {
        public void Configure(EntityTypeBuilder<Referent> builder)
        {
            builder.HasOne(x => x.StageID)
                .WithMany()
                .HasForeignKey(x => x.Stage);
            builder.HasOne(x => x.StageAttributeID)
                .WithMany()
                .HasForeignKey(x => x.StageAttribute);
        }
    }
}

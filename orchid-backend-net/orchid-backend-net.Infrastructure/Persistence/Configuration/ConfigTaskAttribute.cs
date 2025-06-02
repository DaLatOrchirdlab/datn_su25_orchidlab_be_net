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
    internal class ConfigTaskAttribute : IEntityTypeConfiguration<TaskAttribute>
    {
        public void Configure(EntityTypeBuilder<TaskAttribute> builder)
        {
            builder.HasOne(x => x.TaskID)
                .WithMany()
                .HasForeignKey(x => x.Task);
        }
    }
}

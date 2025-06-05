using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigTaskAttribute : IEntityTypeConfiguration<TaskAttribute>
    {
        public void Configure(EntityTypeBuilder<TaskAttribute> builder)
        {
            builder.HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskID);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orchid_backend_net.Domain.Entities;

namespace orchid_backend_net.Infrastructure.Persistence.Configuration
{
    internal class ConfigTaskAssign : IEntityTypeConfiguration<TaskAssign>
    {
        public void Configure(EntityTypeBuilder<TaskAssign> builder)
        {
            builder.HasOne(x => x.Technician)
                .WithMany()
                .HasForeignKey(x => x.TechnicianID);
            builder.HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskID);
        }
    }
}

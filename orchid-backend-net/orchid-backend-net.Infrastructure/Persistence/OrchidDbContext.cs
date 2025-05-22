using Microsoft.EntityFrameworkCore;

namespace orchid_backend_net.Infrastructure.Persistence
{
    public class OrchidDbContext(DbContextOptions<OrchidDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your custom model configurations here
            // setting model creating here
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace orchid_backend_net.Infrastructure.Persistence
{
    public class classOrchidServerDbContextFactory : IDesignTimeDbContextFactory<OrchidServerDbContext>
    {
        public OrchidServerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrchidServerDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Server"));

            return new OrchidServerDbContext(optionsBuilder.Options);
        }
    }
}

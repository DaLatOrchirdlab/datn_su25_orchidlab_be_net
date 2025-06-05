using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace orchid_backend_net.Infrastructure.Persistence
{
    public class OrchidLocalDbContextFactory : IDesignTimeDbContextFactory<OrchidLocalDbContext>
    {
        public OrchidLocalDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrchidLocalDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Lamma-local"));

            return new OrchidLocalDbContext(optionsBuilder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Infrastructure.Persistence;
using orchid_backend_net.Infrastructure.Repository;

namespace orchid_backend_net.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dbType = configuration.GetValue<string>("DatabaseType")?.ToLowerInvariant();
            if (dbType == "postgres")
            {
                services.AddDbContext<OrchidServerDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("Server"), b =>
                    {
                        b.MigrationsAssembly(typeof(OrchidServerDbContext).Assembly.FullName);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                    options.UseLazyLoadingProxies();
                });
                services.AddScoped<IUnitOfWork>(provider => (IUnitOfWork)provider.GetRequiredService<OrchidServerDbContext>());
            }

            if (dbType == "mssql")
            {
                services.AddDbContext<OrchidLocalDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Lamma-local"), b =>
                    {
                        b.MigrationsAssembly(typeof(OrchidLocalDbContext).Assembly.FullName);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                    options.UseLazyLoadingProxies();
                });
                services.AddScoped<IUnitOfWork>(provider => (IUnitOfWork)provider.GetRequiredService<OrchidLocalDbContext>());
            }

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("Redis")["Configuration"];
                options.InstanceName = configuration.GetSection("Redis")["InstanceName"];
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IElementRepositoty, ElementRepository>();
            services.AddScoped<IOrchidAnalyzerService, OrchidAnalyzerService>();
            return services;
        }
    }
}

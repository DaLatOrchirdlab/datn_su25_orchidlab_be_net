using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using orchid_backend_net.Domain.Common.Interfaces;
using orchid_backend_net.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrchidDbContext>((sp, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("Server"),
                    b =>
                    {
                        b.MigrationsAssembly(typeof(OrchidDbContext).Assembly.FullName);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                options.UseLazyLoadingProxies();


            });

            services.AddScoped<IUnitOfWork>(provider => (IUnitOfWork)provider.GetRequiredService<OrchidDbContext>());

            return services;
        }
    }
}

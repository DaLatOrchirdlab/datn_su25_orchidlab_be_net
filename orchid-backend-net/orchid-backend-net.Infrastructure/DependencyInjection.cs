using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Common.Interfaces;
using orchid_backend_net.Domain.IRepositories;
using orchid_backend_net.Infrastructure.Persistence;
using orchid_backend_net.Infrastructure.Repository;
using orchid_backend_net.Infrastructure.Service;

namespace orchid_backend_net.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //database context
            services.AddDbContext<OrchidDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("local"), b =>
                {
                    b.MigrationsAssembly(typeof(OrchidDbContext).Assembly.FullName);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<IUnitOfWork>(provider => (IUnitOfWork)provider.GetRequiredService<OrchidDbContext>());

            //redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("Redis")["Configuration"];
                options.InstanceName = configuration.GetSection("Redis")["InstanceName"];
            });

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<OrchidDbContext>();
                SeedDataGenerator.SeedAsync(dbContext).GetAwaiter().GetResult();
            }

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IElementRepositoty, ElementRepository>();
            services.AddScoped<IExperimentLogRepository, ExperimentLogRepository>();
            services.AddScoped<ILabRoomRepository, LabRoomRepository>();
            services.AddScoped<IMethodRepository, MethodRepository>();
            services.AddScoped<IRepostRepository, RepostRepository>();
            services.AddScoped<ISampleRepository, SampleRepository>();
            services.AddScoped<ISeedlingAttributeRepository,SeedlingAttributeRepository>();
            services.AddScoped<ISeedlingRepository, SeedlingRepository>();
            services.AddScoped<ICharactersicticRepository, CharacteristicRepository>();
            services.AddScoped<IStageRepository, StageRepository>();
            services.AddScoped<ITaskAttributeRepository, TaskAttributeRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskAssignRepository, TaskAssignRepository>();
            services.AddScoped<IHybridizationRepository, HybridizationRepository>();
            services.AddScoped<ILinkedRepository, LinkedRepository>();
            services.AddScoped<ITissueCultureBatchRepository,TissueCultureBatchRepository>();
            services.AddScoped<IOrchidAnalyzerService, OrchidAnalyzerService>();
            services.AddScoped<ICacheService, RedisCacheService>();
            return services;
        }
    }
}

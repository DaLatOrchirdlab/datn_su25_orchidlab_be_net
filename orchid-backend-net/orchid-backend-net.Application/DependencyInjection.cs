using Microsoft.Extensions.DependencyInjection;
using orchid_backend_net.Application.Common.Behaviours;
using orchid_backend_net.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidationProvider, ValidationProvider>();

            return services;
        }
    }
}

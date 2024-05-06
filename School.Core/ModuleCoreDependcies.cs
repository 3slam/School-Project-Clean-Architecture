using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace School.Infrastructure
{
    public static class ModuleCoreDependcies
    {
        public static IServiceCollection AddCoreDependcies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

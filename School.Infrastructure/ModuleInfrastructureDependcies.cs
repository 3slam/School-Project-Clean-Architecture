using Microsoft.Extensions.DependencyInjection;
using School.Infrastructure.IRepository;
using School.Infrastructure.RepositoryImp;

namespace School.Infrastructure
{
    public static class ModuleInfrastructureDependcies
    {
        public static IServiceCollection AddInfrastructureDependcies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using School.Service.IService;
using School.Service.ServiceImp;

namespace School.Service
{
    public static class ModuleServiceDependcies
    {
        public static IServiceCollection AddServiceDependcies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();


            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}

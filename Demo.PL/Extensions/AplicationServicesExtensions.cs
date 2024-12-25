using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.PL.Extensions
{
    public static class AplicationServicesExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;

        }
    }
}

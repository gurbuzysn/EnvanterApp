using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace EnvanterApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(ServiceRegistration).Assembly);
        }
    }
}

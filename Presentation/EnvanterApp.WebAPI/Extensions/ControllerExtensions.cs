using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace EnvanterApp.WebAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddFluentValidation(config =>
                    {
                        config.RegisterValidatorsFromAssemblyContaining<EnvanterApp.Application.Validators.Employees.AddEmployeeValidator>();
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    });

            return services;
        }
    }
}

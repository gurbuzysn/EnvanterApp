using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.Abstractions.Token;
using EnvanterApp.Infrastructure.Services.Minio;
using EnvanterApp.Infrastructure.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace EnvanterApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMinioService, MinioService>();
            services.AddSingleton<IMinioClient, MinioClient>();
        }
    }
}

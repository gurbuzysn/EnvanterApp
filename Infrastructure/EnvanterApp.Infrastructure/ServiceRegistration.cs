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
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMinioService, MinioService>();

            serviceCollection.AddSingleton(new MinioClient()
                .WithEndpoint(configuration["Minio:Endpoint"])
                .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
                .Build());
        }
    }
}

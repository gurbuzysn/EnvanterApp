using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace EnvanterApp.WebAPI.Extensions
{
    public static class MinioExtensions
    {
        public static IServiceCollection AddMinioClient(this IServiceCollection services, IConfiguration configuration)
        {
            var endpoint = configuration["Minio:Endpoint"];
            var accessKey = configuration["Minio:AccessKey"];
            var secretKey = configuration["Minio:SecretKey"];
            var useSSL = bool.Parse(configuration["Minio:UseSSL"] ?? "false");

            services.AddSingleton<IMinioClient>(sp =>
            {
                var client = new MinioClient()
                    .WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey);

                if (useSSL)
                    client = client.WithSSL();

                return client.Build();
            });

            return services;
        }
    }
}

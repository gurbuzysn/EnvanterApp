using EnvanterApp.Application.Abstractions.Token;
using EnvanterApp.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace EnvanterApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}

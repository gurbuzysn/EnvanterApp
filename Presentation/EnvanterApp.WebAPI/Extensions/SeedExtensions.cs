using EnvanterApp.Domain.Entities.Identity;
using EnvanterApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EnvanterApp.WebAPI.Extensions
{
    public static class SeedExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<EnvanterAppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            await EnvanterAppDbContextSeedData.SeedData(context, userManager, roleManager);
        }
    }
}

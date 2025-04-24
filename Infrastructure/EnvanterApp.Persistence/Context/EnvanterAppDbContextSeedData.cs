using EnvanterApp.Domain.Entities.Identity;
using EnvanterApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanterApp.Persistence.Context
{
    public static class EnvanterAppDbContextSeedData
    {
        public static async Task SeedData(EnvanterAppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            await context.Database.MigrateAsync();

            if (await context.AppUsers.AnyAsync() || await context.Roles.AnyAsync())
                return;

            AppUser adminUser = new AppUser()
            {
                Id = Guid.NewGuid(),
                Status = Status.Active,
                CreatedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990,01,01),
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "123456");

            AppRole adminRole = new AppRole()
            {
                Name = "Admin"
            };
            await roleManager.CreateAsync(adminRole);
            await userManager.AddToRoleAsync(adminUser, "Admin");
            context.SaveChanges();
        }
    }
}

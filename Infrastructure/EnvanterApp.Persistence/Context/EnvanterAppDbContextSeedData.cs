using EnvanterApp.Domain.Entities.Identity;
using EnvanterApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanterApp.Persistence.Context
{
    public static class EnvanterAppDbContextSeedData
    {
        public static async Task SeedData(EnvanterAppDbContext context, UserManager<Employee> userManager, RoleManager<AppRole> roleManager)
        {
            await context.Database.MigrateAsync();

            if (await context.Employees.AnyAsync() || await context.Roles.AnyAsync())
                return;

            AppRole adminRole = new AppRole() { Name = "Admin" };
            AppRole employeeRole = new AppRole() { Name = "Employee" };
            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(employeeRole);

            Employee adminUser = new Employee()
            {
                Id = Guid.NewGuid(),
                Status = Status.Active,
                CreatedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                Department = "İdare",
                UserName = "admin@envanterapp.com",
                Email = "admin@envanterapp.com",
                Title = "Yönetici",
                PhoneNumber = "5554443322",
                ImageUri = "http://localhost:9000/profile-images/677cda98-4ee1-4ada-a990-7d05f920f78b.png",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "123456");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

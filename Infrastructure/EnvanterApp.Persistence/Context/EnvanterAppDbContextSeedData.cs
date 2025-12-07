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
            //await context.Database.MigrateAsync();

            if (await context.Employees.AnyAsync() || await context.Roles.AnyAsync())
                return;

            AppRole adminRole = new AppRole() { Name = "Admin", CreatedDate = DateTime.Now };
            AppRole employeeRole = new AppRole() { Name = "Employee", CreatedDate = DateTime.Now };
            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(employeeRole);

            Employee adminUser = new Employee()
            {
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
                ImageUri = "http://localhost:9000/profile-images/Kerem_a344a770-fc6a-4fb2-adc9-9810cb333151.jpg",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, "123456");
            var createdUser = await userManager.FindByNameAsync("admin@envanterapp.com");
            if(createdUser != null)
                await userManager.AddToRoleAsync(createdUser, "Admin");
        }
    }
}

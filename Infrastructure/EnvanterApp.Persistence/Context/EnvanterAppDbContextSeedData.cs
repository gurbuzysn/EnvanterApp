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

            Employee adminUser = new Employee()
            {
                Id = Guid.NewGuid(),
                Status = Status.Active,
                CreatedDate = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1990,01,01),
                Department = Department.Idare,
                UserName = "admin",
                Email = "admin@envanterapp.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "123456");

            AppRole adminRole = new AppRole()
            {
                Name = "Admin"
            };

            AppRole employeeRole = new AppRole()
            {
                Name = "Employee"
            };

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(employeeRole);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

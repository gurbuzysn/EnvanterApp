using EnvanterApp.Domain.Entities.Identity;
using EnvanterApp.Domain.Entities.Items;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Persistence.Context
{
    public class EnvanterAppDbContext : IdentityDbContext<AppUser>
    {
        public EnvanterAppDbContext(DbContextOptions<EnvanterAppDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employes { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<Mouse> Mouses { get; set; }


    }
}

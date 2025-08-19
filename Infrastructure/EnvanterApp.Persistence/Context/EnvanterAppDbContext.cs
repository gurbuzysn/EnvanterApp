using EnvanterApp.Domain.Entities;
using EnvanterApp.Domain.Entities.Identity;
using EnvanterApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Persistence.Context
{
    public class EnvanterAppDbContext : IdentityDbContext<Employee, AppRole, Guid>
    {
        public EnvanterAppDbContext(DbContextOptions<EnvanterAppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}

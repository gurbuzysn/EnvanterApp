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
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>(e =>
            {
                e.HasKey(e => e.Id);
            });

            builder.Entity<AppRole>(r =>
            {
                r.HasKey(r => r.Id);
            });

            builder.Entity<Assignment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.RowId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1);
            });

            builder.Entity<Item>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.RowId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1);
            });

            builder.Entity<Category>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(c => c.RowId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1); ;
            });
        }
    }
}

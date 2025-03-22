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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<Mouse> Mouses { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Admin>().ToTable("Admins");
            builder.Entity<Employee>().ToTable("Employees");

            builder.Entity<Admin>()
                .Property(a => a.FirstName)
                .HasColumnName("FirstName");

            builder.Entity<Admin>()
                .Property(a => a.LastName)
                .HasColumnName("LastName");

            builder.Entity<Admin>()
                .Property(a => a.Status)
                .HasColumnName("Status");

            builder.Entity<Admin>()
                .Property(a => a.CreatedDate)
                .HasColumnName("CreatedDate");

            builder.Entity<Admin>()
                .Property(a => a.ModifiedDate)
                .HasColumnName("ModifiedDate");

            builder.Entity<Admin>()
                .Property(a => a.DeletedDate)
                .HasColumnName("DeletedDate");

            builder.Entity<Admin>()
                .Property(a => a.Gender)
                .HasColumnName("Gender");

            builder.Entity<Admin>()
                .Property(a => a.DateOfBirth)
                .HasColumnName("DateOfBirth");

            builder.Entity<Admin>()
                .Property(a => a.ImageUri)
                .HasColumnName("ImageUri");
        }

    }
}

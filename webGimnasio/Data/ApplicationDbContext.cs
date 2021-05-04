using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using webGimnasio.Models;

namespace webGimnasio.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<webGimnasio.Models.ProjectRole> ProjectRole { get; set; }
        public DbSet<webGimnasio.Models.Clase> Clase { get; set; }
        public DbSet<webGimnasio.Models.ClasesDiarias> ClasesDiarias { get; set; }
        public DbSet<webGimnasio.Models.Pagos> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                    .HasMany(e => e.Roles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
        }
    }
}

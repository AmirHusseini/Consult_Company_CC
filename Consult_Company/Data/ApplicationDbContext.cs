using Consulting_Company.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consult_Company.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Uppdrag> Uppdrags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.firstName)
                .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.lastName)
                .HasMaxLength(250);

        }
    }
}
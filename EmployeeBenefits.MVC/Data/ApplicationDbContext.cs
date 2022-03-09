using EmployeeBenefits.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefits.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependant> Dependants { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Dependant>()
                .HasOne(d => d.Employee)
                .WithMany(e => e.Dependants);

            base.OnModelCreating(builder);
        }
    }
}
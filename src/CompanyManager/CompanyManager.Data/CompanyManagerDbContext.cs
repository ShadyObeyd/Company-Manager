using CompanyManager.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Data
{
    public class CompanyManagerDbContext : DbContext
    {
        public CompanyManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public CompanyManagerDbContext()
        {

        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data
{
    public class DemoBackendDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public DemoBackendDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);         
        }
    }
}
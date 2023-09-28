using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data
{
    public class DemoBackendDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<StoreCategory> StoreCategory { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLines> OrderLines { get; set; }

        public DemoBackendDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasMany<ProductCategory>(x => x.ProductCategories)
                      .WithOne(x => x.Store)
                      .HasForeignKey(x => x.StoreId)
                      .OnDelete(DeleteBehavior.ClientCascade);


                entity.HasOne(x => x.StoreCategory)
                      .WithOne(x => x.Store)
                      .HasForeignKey<Store>(x => x.StoreCategoryId)
                      .IsRequired();

                entity.HasOne(x => x.Address)
                      .WithOne(x => x.Store)
                      .HasForeignKey<Store>(x => x.AddressId)
                      .IsRequired();

                entity.HasMany<Order>(x => x.Orders)
                      .WithOne(x => x.Store)
                      .HasForeignKey(x => x.StoreId)
                      .OnDelete(DeleteBehavior.ClientCascade);

            });

            modelBuilder.Entity<ProductCategory>()
                .HasMany<Product>(x => x.Products)
                .WithOne(x => x.ProductCategory)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<Product>()
                .HasMany<OrderLines>(x => x.OrderLines)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<Order>()
                .HasMany<OrderLines>(x => x.OrderLines)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<Address>()
                .HasOne(x => x.Order)
                .WithOne(x => x.Address)
                .HasForeignKey<Order>(x => x.AddressId)
                .IsRequired();

            modelBuilder.Entity<Store>().ToTable("Stores");
            modelBuilder.Entity<StoreCategory>().ToTable("StoreCategories");
            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderLines>().ToTable("OrderLines");
        }
    }
}
using Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Entities.Auth;
using Models.Entities.Translations;

namespace Data
{
    public class DemoBackendDbContext : IdentityDbContext<AuthUser>
    //public class DemoBackendDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<StoreCategory> StoreCategory { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<OrderLines> OrderLines { get; set; }
        public DbSet<Translations> Translations { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }

        public DemoBackendDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply HasQueryFilter on all queries for the entities that implement the ISoftDeletable Interface
            modelBuilder.SetQueryFilterToByDefaultExcludeDeletedEntries();

            //Application Tables
            modelBuilder.Entity<Translations>()
                .HasKey(o => new { o.Key, o.LanguageIdentifier });
            modelBuilder.Entity<Translations>()
                .Property(x => x.Key).HasMaxLength(200);
            modelBuilder.Entity<Translations>()
                .Property(x => x.LanguageIdentifier).HasMaxLength(7);
            modelBuilder.Entity<Translations>()
                .Property(x => x.TranslatedText).HasMaxLength(8000);

            modelBuilder.Entity<EmailTemplate>()
                .HasIndex(x => new { x.EmailType })
                .IsUnique();


            // Talbes to correct Schema
            modelBuilder.Entity<Translations>().ToTable("Translations", "Application");
            modelBuilder.Entity<EmailTemplate>().ToTable("EmailTemplate", "Application");

            // Identity Tables
            modelBuilder.Entity<AuthUser>().ToTable("Users", "Identity");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Identity");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Identity");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Identity");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Identity");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Identity");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Identity");

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

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasMany<Address>(x => x.Addresses)
                      .WithOne(x => x.Customer)
                      .HasForeignKey(x => x.CustomerId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.ClientCascade);

                entity.HasMany<Order>(x => x.Orders)
                      .WithOne(x => x.Customer)
                      .HasForeignKey(x => x.CustomerId)
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
                .HasMany<Order>(x => x.Order)
                .WithOne(x => x.Address)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<Store>().ToTable("Stores");
            modelBuilder.Entity<StoreCategory>().ToTable("StoreCategories");
            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderLines>().ToTable("OrderLines");           
            modelBuilder.Entity<Customer>().ToTable("Customers");           
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data
{
    public class DemoBackendDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Pelatis> Pelatis { get; set; }
        public DbSet<Kaseta> Kaseta { get; set; }
        public DbSet<Sintelestis> Sintelestis { get; set; }
        public DbSet<Tainia> Tainia { get; set; }
        public DbSet<Enoikiasi> Enoikiasi { get; set; }
        public DbSet<Tn_sn> Tn_sn { get; set; }

        public DemoBackendDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tainia>()
                .HasMany<Kaseta>(x => x.Kasetes)
                .WithOne(x => x.Tainia)
                .HasForeignKey(x => x.IDTainias)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tn_sn>(entity =>
            {
                entity.HasKey(x => new { x.IDTainias, x.IDSintelesti });

                entity.HasOne(x => x.Sintelestis)
                      .WithMany(x => x.TainiesSintelestes)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasForeignKey(x => x.IDSintelesti);

                entity.HasOne(x => x.Tainia)
                      .WithMany(x => x.TainiesSintelestes)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasForeignKey(x => x.IDTainias);
            });

            modelBuilder.Entity<Enoikiasi>(entity =>
            {
                entity.HasKey(x => new { x.IDKasetas, x.IDPelati, x.Apo });

                entity.HasOne(x => x.Pelatis)
                      .WithMany(x => x.Enoikiasis)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasForeignKey(x => x.IDPelati);

                entity.HasOne(x => x.Kaseta)
                      .WithMany(x => x.Enoikiasis)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasForeignKey(x => x.IDKasetas);
            });

            modelBuilder.Entity<Kaseta>().ToTable("Kasetes");
            modelBuilder.Entity<Pelatis>().ToTable("Pelatis");
            modelBuilder.Entity<Sintelestis>().ToTable("Sintelestis");
            modelBuilder.Entity<Tainia>().ToTable("Tainia");
            modelBuilder.Entity<Enoikiasi>().ToTable("Enoikiasi");
            modelBuilder.Entity<Tn_sn>().ToTable("Tn_sn");
        }
    }
}
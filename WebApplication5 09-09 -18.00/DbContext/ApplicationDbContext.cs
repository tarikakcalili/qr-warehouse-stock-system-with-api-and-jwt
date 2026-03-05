using MegaQr.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaQr.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.Product)
                .WithMany(s => s.Stores)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.Section)
                .WithMany(s => s.Stores)
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Models;

namespace ShoppingListApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }
        public DbSet<Supermarket> Supermarkets { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<ProductTranslation>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(p => p.Id);

            modelBuilder.Entity<CategoryTranslation>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.Id);
        }
    }
}

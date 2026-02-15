using EFCore_Relationships.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Relationships.DATA;

/// <summary>
/// Application database context for managing entities
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }


    /// <summary>
    /// Configure entity relationships and constraints
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Product Configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductId);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(p => p.Stock).IsRequired();
            entity.Property(p => p.CreatedAt).IsRequired();

            // Index for better query performance
            entity.HasIndex(p => p.Name);
            entity.HasIndex(p => p.Price);
        });

        // Order Configuration
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
            entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(o => o.CustomerEmail).IsRequired().HasMaxLength(100);
            entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(o => o.OrderDate).IsRequired();
            entity.Property(o => o.Status).IsRequired().HasMaxLength(50);

            // Index for better query performance
            entity.HasIndex(o => o.CustomerEmail);
            entity.HasIndex(o => o.Status);
            entity.HasIndex(o => o.OrderDate);
        });
    }
}
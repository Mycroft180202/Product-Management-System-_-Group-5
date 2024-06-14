using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Product_Management_System.Models;

namespace Product_Management_System.Data;

public partial class ProductManagementDbContext : DbContext
{
    public ProductManagementDbContext()
    {
    }

    public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCostHistory> ProductCostHistories { get; set; }

    public virtual DbSet<ProductInventory> ProductInventories { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }

    public virtual DbSet<ProductSubcategory> ProductSubcategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA47773AAF222");

            entity.ToTable("Location");

            entity.Property(e => e.LocationId)
                .ValueGeneratedNever()
                .HasColumnName("LocationID");
            entity.Property(e => e.Availability).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.CostRate).HasColumnType("smallmoney");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED90F23CBA");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("ProductID");
            entity.Property(e => e.Color).HasMaxLength(15);
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.SellEndDate).HasColumnType("datetime");
            entity.Property(e => e.SellStartDate).HasColumnType("datetime");
            entity.Property(e => e.SubcategoryId).HasColumnName("SubcategoryID");

            entity.HasOne(d => d.Model).WithMany(p => p.Products)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK__Product__ModelID__29572725");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubcategoryId)
                .HasConstraintName("FK__Product__Subcate__286302EC");
        });

        modelBuilder.Entity<ProductCostHistory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.StartDate }).HasName("PK__ProductC__D0ED59229D9853B7");

            entity.ToTable("ProductCostHistory");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.EndDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCostHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductCo__Produ__31EC6D26");
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.LocationId }).HasName("PK__ProductI__DA732CAAEF93DFA5");

            entity.ToTable("ProductInventory");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Shelf).HasMaxLength(10);

            entity.HasOne(d => d.Location).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Locat__2F10007B");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Produ__2E1BDC42");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__ProductM__E8D7A1CCE8604A7F");

            entity.ToTable("ProductModel");

            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasColumnName("ModelID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductPriceHistory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.StartDate }).HasName("PK__ProductP__D0ED59220A66D7C7");

            entity.ToTable("ProductPriceHistory");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPriceHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPr__Produ__34C8D9D1");
        });

        modelBuilder.Entity<ProductSubcategory>(entity =>
        {
            entity.HasKey(e => e.SubcategoryId).HasName("PK__ProductS__9C4E707DC8D2AAE6");

            entity.ToTable("ProductSubcategory");

            entity.Property(e => e.SubcategoryId)
                .ValueGeneratedNever()
                .HasColumnName("SubcategoryID");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

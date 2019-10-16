using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeGeneration.Repositories.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<CategoryDAO> Category { get; set; }
        public virtual DbSet<Category_ItemDAO> Category_Item { get; set; }
        public virtual DbSet<ItemDAO> Item { get; set; }
        public virtual DbSet<UserDAO> User { get; set; }
        public virtual DbSet<WarehouseDAO> Warehouse { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=.;initial catalog=WeGift;persist security info=True;user id=sa;password=123456a@;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CategoryDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<Category_ItemDAO>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ItemId });

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Category_Items)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Item_Category");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Category_Items)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Item_Item");
            });

            modelBuilder.Entity<ItemDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UserDAO>(entity =>
            {
                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.Username).HasMaxLength(500);
            });

            modelBuilder.Entity<WarehouseDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehouse_User");
            });

            OnModelCreatingExt(modelBuilder);
        }

        partial void OnModelCreatingExt(ModelBuilder modelBuilder);
    }
}

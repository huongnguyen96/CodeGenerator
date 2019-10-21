using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeGeneration.Repositories.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<AdministratorDAO> Administrator { get; set; }
        public virtual DbSet<BrandDAO> Brand { get; set; }
        public virtual DbSet<CategoryDAO> Category { get; set; }
        public virtual DbSet<CustomerDAO> Customer { get; set; }
        public virtual DbSet<CustomerGroupingDAO> CustomerGrouping { get; set; }
        public virtual DbSet<Customer_CustomerGroupingDAO> Customer_CustomerGrouping { get; set; }
        public virtual DbSet<DiscountDAO> Discount { get; set; }
        public virtual DbSet<DiscountCustomerGroupingDAO> DiscountCustomerGrouping { get; set; }
        public virtual DbSet<DiscountItemDAO> DiscountItem { get; set; }
        public virtual DbSet<DistrictDAO> District { get; set; }
        public virtual DbSet<ImageFileDAO> ImageFile { get; set; }
        public virtual DbSet<ItemDAO> Item { get; set; }
        public virtual DbSet<ItemStatusDAO> ItemStatus { get; set; }
        public virtual DbSet<ItemTypeDAO> ItemType { get; set; }
        public virtual DbSet<OrderDAO> Order { get; set; }
        public virtual DbSet<OrderContentDAO> OrderContent { get; set; }
        public virtual DbSet<PartnerDAO> Partner { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<ShippingAddressDAO> ShippingAddress { get; set; }
        public virtual DbSet<StockDAO> Stock { get; set; }
        public virtual DbSet<UnitDAO> Unit { get; set; }
        public virtual DbSet<VariationDAO> Variation { get; set; }
        public virtual DbSet<VariationGroupingDAO> VariationGrouping { get; set; }
        public virtual DbSet<WardDAO> Ward { get; set; }
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

            modelBuilder.Entity<AdministratorDAO>(entity =>
            {
                entity.Property(e => e.DisplayName).HasMaxLength(500);

                entity.Property(e => e.Username).HasMaxLength(500);
            });

            modelBuilder.Entity<BrandDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brand_Category");
            });

            modelBuilder.Entity<CategoryDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.Icon).HasMaxLength(2000);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<CustomerDAO>(entity =>
            {
                entity.Property(e => e.DisplayName).HasMaxLength(500);

                entity.Property(e => e.Username).HasMaxLength(500);
            });

            modelBuilder.Entity<CustomerGroupingDAO>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Customer_CustomerGroupingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CustomerGroupingId });

                entity.HasOne(d => d.CustomerGrouping)
                    .WithMany(p => p.Customer_CustomerGroupings)
                    .HasForeignKey(d => d.CustomerGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CustomerGrouping_CustomerGrouping");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Customer_CustomerGroupings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CustomerGrouping_Customer");
            });

            modelBuilder.Entity<DiscountDAO>(entity =>
            {
                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<DiscountCustomerGroupingDAO>(entity =>
            {
                entity.Property(e => e.CustomerGroupingCode)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountCustomerGroupings)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignCustomerGrouping_Campaign");
            });

            modelBuilder.Entity<DiscountItemDAO>(entity =>
            {
                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountItems)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignItem_Campaign");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.DiscountItems)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignItem_ItemVersion");
            });

            modelBuilder.Entity<DistrictDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<ImageFileDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(2000);
            });

            modelBuilder.Entity<ItemDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.SKU).HasMaxLength(500);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Category");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Merchant");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_ItemStatus");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_ItemType");
            });

            modelBuilder.Entity<ItemStatusDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ItemTypeDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDAO>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.VoucherCode).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<OrderContentDAO>(entity =>
            {
                entity.Property(e => e.FirstVersion).HasMaxLength(500);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SecondVersion).HasMaxLength(500);

                entity.Property(e => e.ThirdVersion).HasMaxLength(500);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderContents)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderContent_Order");
            });

            modelBuilder.Entity<PartnerDAO>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ContactPerson).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(500);
            });

            modelBuilder.Entity<ProvinceDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ShippingAddressDAO>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShippingAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingAddress_Customer");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.ShippingAddresses)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingAddress_District");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.ShippingAddresses)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingAddress_Province");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.ShippingAddresses)
                    .HasForeignKey(d => d.WardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingAddress_Ward");
            });

            modelBuilder.Entity<StockDAO>(entity =>
            {
                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitStock_Unit");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemStock_Warehouse");
            });

            modelBuilder.Entity<UnitDAO>(entity =>
            {
                entity.Property(e => e.SKU)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FirstVariation)
                    .WithMany(p => p.UnitFirstVariations)
                    .HasForeignKey(d => d.FirstVariationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Unit_ItemTier");

                entity.HasOne(d => d.SecondVariation)
                    .WithMany(p => p.UnitSecondVariations)
                    .HasForeignKey(d => d.SecondVariationId)
                    .HasConstraintName("FK_Unit_ItemTier1");

                entity.HasOne(d => d.ThirdVariation)
                    .WithMany(p => p.UnitThirdVariations)
                    .HasForeignKey(d => d.ThirdVariationId)
                    .HasConstraintName("FK_ItemVersion_Version");
            });

            modelBuilder.Entity<VariationDAO>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.VariationGrouping)
                    .WithMany(p => p.Variations)
                    .HasForeignKey(d => d.VariationGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTier_ItemTierGrouping");
            });

            modelBuilder.Entity<VariationGroupingDAO>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.VariationGroupings)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTierGrouping_Item");
            });

            modelBuilder.Entity<WardDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ward_District");
            });

            modelBuilder.Entity<WarehouseDAO>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(500);

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehouse_Merchant");
            });

            OnModelCreatingExt(modelBuilder);
        }

        partial void OnModelCreatingExt(ModelBuilder modelBuilder);
    }
}

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
        public virtual DbSet<DiscountContentDAO> DiscountContent { get; set; }
        public virtual DbSet<Discount_CustomerGroupingDAO> Discount_CustomerGrouping { get; set; }
        public virtual DbSet<DistrictDAO> District { get; set; }
        public virtual DbSet<EVoucherDAO> EVoucher { get; set; }
        public virtual DbSet<EVoucherContentDAO> EVoucherContent { get; set; }
        public virtual DbSet<ImageFileDAO> ImageFile { get; set; }
        public virtual DbSet<ItemDAO> Item { get; set; }
        public virtual DbSet<MerchantDAO> Merchant { get; set; }
        public virtual DbSet<MerchantAddressDAO> MerchantAddress { get; set; }
        public virtual DbSet<OrderDAO> Order { get; set; }
        public virtual DbSet<OrderContentDAO> OrderContent { get; set; }
        public virtual DbSet<OrderStatusDAO> OrderStatus { get; set; }
        public virtual DbSet<PaymentMethodDAO> PaymentMethod { get; set; }
        public virtual DbSet<ProductDAO> Product { get; set; }
        public virtual DbSet<ProductStatusDAO> ProductStatus { get; set; }
        public virtual DbSet<ProductTypeDAO> ProductType { get; set; }
        public virtual DbSet<Product_MerchantAddressDAO> Product_MerchantAddress { get; set; }
        public virtual DbSet<Product_PaymentMethodDAO> Product_PaymentMethod { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<ShippingAddressDAO> ShippingAddress { get; set; }
        public virtual DbSet<StockDAO> Stock { get; set; }
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

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.PhoneNumber).HasMaxLength(500);

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

            modelBuilder.Entity<DiscountContentDAO>(entity =>
            {
                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountContents)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignItem_Campaign");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.DiscountContents)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignItem_ItemVersion");
            });

            modelBuilder.Entity<Discount_CustomerGroupingDAO>(entity =>
            {
                entity.HasKey(e => new { e.DiscountId, e.CustomerGroupingId });

                entity.HasOne(d => d.CustomerGrouping)
                    .WithMany(p => p.Discount_CustomerGroupings)
                    .HasForeignKey(d => d.CustomerGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discount_CustomerGrouping_CustomerGrouping");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Discount_CustomerGroupings)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discount_CustomerGrouping_Discount");
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

            modelBuilder.Entity<EVoucherDAO>(entity =>
            {
                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.EVouchers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVoucher_Customer");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.EVouchers)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_EVoucher_Product");
            });

            modelBuilder.Entity<EVoucherContentDAO>(entity =>
            {
                entity.Property(e => e.MerchantCode).HasMaxLength(500);

                entity.Property(e => e.UsedCode).HasMaxLength(500);

                entity.Property(e => e.UsedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EVourcher)
                    .WithMany(p => p.EVoucherContents)
                    .HasForeignKey(d => d.EVourcherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVoucherContent_EVoucher");
            });

            modelBuilder.Entity<ImageFileDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(2000);
            });

            modelBuilder.Entity<ItemDAO>(entity =>
            {
                entity.Property(e => e.SKU)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FirstVariation)
                    .WithMany(p => p.ItemFirstVariations)
                    .HasForeignKey(d => d.FirstVariationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Unit_ItemTier");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Product");

                entity.HasOne(d => d.SecondVariation)
                    .WithMany(p => p.ItemSecondVariations)
                    .HasForeignKey(d => d.SecondVariationId)
                    .HasConstraintName("FK_Unit_ItemTier1");
            });

            modelBuilder.Entity<MerchantDAO>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ContactPerson).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(500);
            });

            modelBuilder.Entity<MerchantAddressDAO>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.Contact).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(500);

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.MerchantAddresses)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchantAddress_Merchant");
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

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderStatus");
            });

            modelBuilder.Entity<OrderContentDAO>(entity =>
            {
                entity.Property(e => e.FirstVersion).HasMaxLength(500);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SecondVersion).HasMaxLength(500);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderContents)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_OrderContent_Item");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderContents)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderContent_Order");
            });

            modelBuilder.Entity<OrderStatusDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<PaymentMethodDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ProductDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Category");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Merchant");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_ItemStatus");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_ItemType");
            });

            modelBuilder.Entity<ProductStatusDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductTypeDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Product_MerchantAddressDAO>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.MerchantAddressId });

                entity.HasOne(d => d.MerchantAddress)
                    .WithMany(p => p.Product_MerchantAddresses)
                    .HasForeignKey(d => d.MerchantAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_MerchantAddress_MerchantAddress");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Product_MerchantAddresses)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_MerchantAddress_Product");
            });

            modelBuilder.Entity<Product_PaymentMethodDAO>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.PaymentMethodId });

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Product_PaymentMethods)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_PaymentMethod_PaymentMethod");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Product_PaymentMethods)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_PaymentMethod_Product");
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
                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitStock_Unit");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemStock_Warehouse");
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

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.VariationGroupings)
                    .HasForeignKey(d => d.ProductId)
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

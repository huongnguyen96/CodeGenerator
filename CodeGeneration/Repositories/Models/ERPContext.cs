using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeGeneration.Repositories.Models
{
    public partial class ERPContext : DbContext
    {
        public virtual DbSet<APPSPermissionDAO> APPSPermission { get; set; }
        public virtual DbSet<AccountingPeriodDAO> AccountingPeriod { get; set; }
        public virtual DbSet<AssetDAO> Asset { get; set; }
        public virtual DbSet<AssetOrganizationDAO> AssetOrganization { get; set; }
        public virtual DbSet<Asset_AssetOrganizationDAO> Asset_AssetOrganization { get; set; }
        public virtual DbSet<AuditLogDAO> AuditLog { get; set; }
        public virtual DbSet<BankDAO> Bank { get; set; }
        public virtual DbSet<BankAccountDAO> BankAccount { get; set; }
        public virtual DbSet<BusinessGroupDAO> BusinessGroup { get; set; }
        public virtual DbSet<COATemplateDAO> COATemplate { get; set; }
        public virtual DbSet<COATemplateDetailDAO> COATemplateDetail { get; set; }
        public virtual DbSet<ChartOfAccountDAO> ChartOfAccount { get; set; }
        public virtual DbSet<CostCenterDAO> CostCenter { get; set; }
        public virtual DbSet<CurrencyDAO> Currency { get; set; }
        public virtual DbSet<CustomerDAO> Customer { get; set; }
        public virtual DbSet<CustomerBankAccountDAO> CustomerBankAccount { get; set; }
        public virtual DbSet<CustomerContactDAO> CustomerContact { get; set; }
        public virtual DbSet<CustomerDetailDAO> CustomerDetail { get; set; }
        public virtual DbSet<CustomerDetail_CustomerGroupingDAO> CustomerDetail_CustomerGrouping { get; set; }
        public virtual DbSet<CustomerGroupingDAO> CustomerGrouping { get; set; }
        public virtual DbSet<DivisionDAO> Division { get; set; }
        public virtual DbSet<EmployeeDAO> Employee { get; set; }
        public virtual DbSet<EmployeeContactDAO> EmployeeContact { get; set; }
        public virtual DbSet<EmployeeDetailDAO> EmployeeDetail { get; set; }
        public virtual DbSet<EmployeePositionDAO> EmployeePosition { get; set; }
        public virtual DbSet<Employee_HROrganizationDAO> Employee_HROrganization { get; set; }
        public virtual DbSet<Employee_InventoryOrganizationAddressDAO> Employee_InventoryOrganizationAddress { get; set; }
        public virtual DbSet<Employee_ProjectOrganizationDAO> Employee_ProjectOrganization { get; set; }
        public virtual DbSet<EnumMasterDataDAO> EnumMasterData { get; set; }
        public virtual DbSet<FeatureDAO> Feature { get; set; }
        public virtual DbSet<FeatureOperationDAO> FeatureOperation { get; set; }
        public virtual DbSet<FeaturePermissionDAO> FeaturePermission { get; set; }
        public virtual DbSet<FiscalYearDAO> FiscalYear { get; set; }
        public virtual DbSet<GeneralPriceRateDAO> GeneralPriceRate { get; set; }
        public virtual DbSet<HROrganizationDAO> HROrganization { get; set; }
        public virtual DbSet<InventoryOrganizationDAO> InventoryOrganization { get; set; }
        public virtual DbSet<InventoryOrganizationAddressDAO> InventoryOrganizationAddress { get; set; }
        public virtual DbSet<ItemDAO> Item { get; set; }
        public virtual DbSet<ItemDetailDAO> ItemDetail { get; set; }
        public virtual DbSet<ItemDetail_ItemGroupingDAO> ItemDetail_ItemGrouping { get; set; }
        public virtual DbSet<ItemDiscountDAO> ItemDiscount { get; set; }
        public virtual DbSet<ItemGroupingDAO> ItemGrouping { get; set; }
        public virtual DbSet<ItemMaterialDAO> ItemMaterial { get; set; }
        public virtual DbSet<JobLevelDAO> JobLevel { get; set; }
        public virtual DbSet<JobTitleDAO> JobTitle { get; set; }
        public virtual DbSet<LegalEntityDAO> LegalEntity { get; set; }
        public virtual DbSet<NotificationDAO> Notification { get; set; }
        public virtual DbSet<OperationDAO> Operation { get; set; }
        public virtual DbSet<PaymentMethodDAO> PaymentMethod { get; set; }
        public virtual DbSet<PaymentTermDAO> PaymentTerm { get; set; }
        public virtual DbSet<PositionDAO> Position { get; set; }
        public virtual DbSet<ProjectOrganizationDAO> ProjectOrganization { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<SetOfBookDAO> SetOfBook { get; set; }
        public virtual DbSet<SupplierDAO> Supplier { get; set; }
        public virtual DbSet<SupplierBankAccountDAO> SupplierBankAccount { get; set; }
        public virtual DbSet<SupplierContactDAO> SupplierContact { get; set; }
        public virtual DbSet<SupplierDetailDAO> SupplierDetail { get; set; }
        public virtual DbSet<SupplierDetail_SupplierGroupingDAO> SupplierDetail_SupplierGrouping { get; set; }
        public virtual DbSet<SupplierGroupingDAO> SupplierGrouping { get; set; }
        public virtual DbSet<SystemConfigurationDAO> SystemConfiguration { get; set; }
        public virtual DbSet<TaxDAO> Tax { get; set; }
        public virtual DbSet<TaxTemplateDAO> TaxTemplate { get; set; }
        public virtual DbSet<TaxTemplateDetailDAO> TaxTemplateDetail { get; set; }
        public virtual DbSet<TransformationUnitDAO> TransformationUnit { get; set; }
        public virtual DbSet<UnitOfMeasureDAO> UnitOfMeasure { get; set; }
        public virtual DbSet<UserDAO> User { get; set; }
        public virtual DbSet<UserProfileDAO> UserProfile { get; set; }
        public virtual DbSet<VoucherDAO> Voucher { get; set; }
        public virtual DbSet<VoucherTypeDAO> VoucherType { get; set; }

        public ERPContext(DbContextOptions<ERPContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=.;initial catalog=ERP;persist security info=True;user id=sa;password=123456a@;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<APPSPermissionDAO>(entity =>
            {
                entity.ToTable("APPSPermission", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_APPSPermission")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.APPSPermissions)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .HasConstraintName("FK_APPSPermission_BusinessGroup");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.APPSPermissions)
                    .HasForeignKey(d => d.DivisionId)
                    .HasConstraintName("FK_APPSPermission_Division");

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.APPSPermissions)
                    .HasForeignKey(d => d.LegalEntityId)
                    .HasConstraintName("FK_APPSPermission_LegalEntity");

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.APPSPermissions)
                    .HasForeignKey(d => d.SetOfBookId)
                    .HasConstraintName("FK_APPSPermission_SetOfBook");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.APPSPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPSPermission_User");
            });

            modelBuilder.Entity<AccountingPeriodDAO>(entity =>
            {
                entity.ToTable("AccountingPeriod", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_AccountingPeriod")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.EndPeriod).HasColumnType("date");

                entity.Property(e => e.StartPeriod).HasColumnType("date");

                entity.HasOne(d => d.FiscalYear)
                    .WithMany(p => p.AccountingPeriods)
                    .HasForeignKey(d => d.FiscalYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountingPeriod_FiscalYear");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AccountingPeriods)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountingPeriod_EnumMasterData");
            });

            modelBuilder.Entity<AssetDAO>(entity =>
            {
                entity.ToTable("Asset", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Asset")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_BusinessGroup");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AssetStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_EnumMasterData1");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AssetTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_EnumMasterData");
            });

            modelBuilder.Entity<AssetOrganizationDAO>(entity =>
            {
                entity.ToTable("AssetOrganization", "APPS");

                entity.HasIndex(e => new { e.Code, e.DivisionId })
                    .HasName("CX_Code_AssetOrganization")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.AssetOrganizations)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetOrganization_Division");
            });

            modelBuilder.Entity<Asset_AssetOrganizationDAO>(entity =>
            {
                entity.HasKey(e => new { e.AssetOrganizationId, e.AssetId })
                    .HasAnnotation("SqlServer:Clustered", false);

                entity.ToTable("Asset_AssetOrganization", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Asset_AssetOrganization")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Asset_AssetOrganizations)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_AssetOrganization_Asset");

                entity.HasOne(d => d.AssetOrganization)
                    .WithMany(p => p.Asset_AssetOrganizations)
                    .HasForeignKey(d => d.AssetOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_AssetOrganization_AssetOrganization");
            });

            modelBuilder.Entity<AuditLogDAO>(entity =>
            {
                entity.ToTable("AuditLog", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_AuditLog")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.NewData).HasColumnType("ntext");

                entity.Property(e => e.OldData).HasColumnType("ntext");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditLog_User");
            });

            modelBuilder.Entity<BankDAO>(entity =>
            {
                entity.ToTable("Bank", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Bank")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.Banks)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bank_BusinessGroup");
            });

            modelBuilder.Entity<BankAccountDAO>(entity =>
            {
                entity.ToTable("BankAccount", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_BankAccount")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.No)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccount_Bank");

                entity.HasOne(d => d.ChartOfAccount)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.ChartOfAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccount_ChartOfAccount");

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccount_SetOfBook");
            });

            modelBuilder.Entity<BusinessGroupDAO>(entity =>
            {
                entity.ToTable("BusinessGroup", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_BusinessGroup")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => e.Code)
                    .HasName("IX_Code_BusinessGroup")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<COATemplateDAO>(entity =>
            {
                entity.ToTable("COATemplate", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_COATemplate")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.COATemplates)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COATemplate_BusinessGroup");
            });

            modelBuilder.Entity<COATemplateDetailDAO>(entity =>
            {
                entity.ToTable("COATemplateDetail", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_COATemplateDetail")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.COATemplate)
                    .WithMany(p => p.COATemplateDetails)
                    .HasForeignKey(d => d.COATemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COATemplateDetail_COATemplate");
            });

            modelBuilder.Entity<ChartOfAccountDAO>(entity =>
            {
                entity.ToTable("ChartOfAccount", "APPS");

                entity.HasIndex(e => e.AccountCode)
                    .HasName("IX_ChartOfAccount")
                    .IsUnique();

                entity.HasIndex(e => e.CX)
                    .HasName("CX_ChartOfAccount")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AccountDescription).HasMaxLength(2000);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.AliasCode).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CharacteristicNavigation)
                    .WithMany(p => p.ChartOfAccounts)
                    .HasForeignKey(d => d.Characteristic)
                    .HasConstraintName("FK_ChartOfAccount_EnumMasterData");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ChartOfAccount_ChartOfAccount");

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.ChartOfAccounts)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChartOfAccount_SetofBook");
            });

            modelBuilder.Entity<CostCenterDAO>(entity =>
            {
                entity.ToTable("CostCenter", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_CostCenter")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.ChartOfAccount)
                    .WithMany(p => p.CostCenters)
                    .HasForeignKey(d => d.ChartOfAccountId)
                    .HasConstraintName("FK_CostCenter_ChartOfAccount");

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.CostCenters)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostCenter_SetOfBook");
            });

            modelBuilder.Entity<CurrencyDAO>(entity =>
            {
                entity.ToTable("Currency", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_FunctionalCurrency")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => e.Code)
                    .HasName("IX_Code_Currency")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code).HasMaxLength(3);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.Currencies)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Currency_BusinessGroup");
            });

            modelBuilder.Entity<CustomerDAO>(entity =>
            {
                entity.ToTable("Customer", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.TaxCode).HasMaxLength(100);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_BusinessGroup");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_EnumMasterData");
            });

            modelBuilder.Entity<CustomerBankAccountDAO>(entity =>
            {
                entity.ToTable("CustomerBankAccount", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Branch)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.CustomerBankAccounts)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerBankAccount_Bank");

                entity.HasOne(d => d.CustomerDetail)
                    .WithMany(p => p.CustomerBankAccounts)
                    .HasForeignKey(d => d.CustomerDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerBankAccount_CustomerDetail");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.CustomerBankAccounts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerBankAccount_Province");
            });

            modelBuilder.Entity<CustomerContactDAO>(entity =>
            {
                entity.ToTable("CustomerContact", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.CustomerDetail)
                    .WithMany(p => p.CustomerContacts)
                    .HasForeignKey(d => d.CustomerDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerContact_CustomerDetail");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.CustomerContacts)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_CustomerContact_Province");
            });

            modelBuilder.Entity<CustomerDetailDAO>(entity =>
            {
                entity.ToTable("CustomerDetail", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.DebtLoad).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerDetail_Customer");

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.CustomerDetails)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerDetail_LegalEntity");

                entity.HasOne(d => d.PaymentTerm)
                    .WithMany(p => p.CustomerDetails)
                    .HasForeignKey(d => d.PaymentTermId)
                    .HasConstraintName("FK_CustomerDetail_PaymentTerm");

                entity.HasOne(d => d.StaffInCharge)
                    .WithMany(p => p.CustomerDetails)
                    .HasForeignKey(d => d.StaffInChargeId)
                    .HasConstraintName("FK_CustomerDetail_Employee");
            });

            modelBuilder.Entity<CustomerDetail_CustomerGroupingDAO>(entity =>
            {
                entity.ToTable("CustomerDetail_CustomerGrouping", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CustomerDetail)
                    .WithMany(p => p.CustomerDetail_CustomerGroupings)
                    .HasForeignKey(d => d.CustomerDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CustomerGrouping_CustomerDetail");

                entity.HasOne(d => d.CustomerGrouping)
                    .WithMany(p => p.CustomerDetail_CustomerGroupings)
                    .HasForeignKey(d => d.CustomerGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CustomerGrouping_CustomerGrouping");
            });

            modelBuilder.Entity<CustomerGroupingDAO>(entity =>
            {
                entity.ToTable("CustomerGrouping", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.CustomerGroupings)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerGroup_LegalEntity");
            });

            modelBuilder.Entity<DivisionDAO>(entity =>
            {
                entity.ToTable("Division", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Division")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.LegalEntityId })
                    .HasName("IX_Code_Division")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.Divisions)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Division_LegalEntity");
            });

            modelBuilder.Entity<EmployeeDAO>(entity =>
            {
                entity.ToTable("Employee", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Employee")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.IdNumber).HasMaxLength(20);

                entity.Property(e => e.IssueDate).HasColumnType("date");

                entity.Property(e => e.IssueLocation).HasMaxLength(100);

                entity.Property(e => e.TaxCode).HasMaxLength(100);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_BusinessGroup");

                entity.HasOne(d => d.JobLevel)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobLevelId)
                    .HasConstraintName("FK_Employee_JobLevel");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("FK_Employee_JobTitle");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_EnumMasterData");
            });

            modelBuilder.Entity<EmployeeContactDAO>(entity =>
            {
                entity.ToTable("EmployeeContact", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EmployeeDetail)
                    .WithMany(p => p.EmployeeContacts)
                    .HasForeignKey(d => d.EmployeeDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeContact_EmployeeDetail");
            });

            modelBuilder.Entity<EmployeeDetailDAO>(entity =>
            {
                entity.ToTable("EmployeeDetail", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BankAccountName).HasMaxLength(500);

                entity.Property(e => e.BankAccountNumber).HasMaxLength(500);

                entity.Property(e => e.BankAddress).HasMaxLength(500);

                entity.Property(e => e.BankBranch).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.EffectiveDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.EmployeeDetails)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_EmployeeDetail_Bank");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDetail_Employee");

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.EmployeeDetails)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDetail_LegalEntity");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.EmployeeDetails)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_EmployeeDetail_Province");
            });

            modelBuilder.Entity<EmployeePositionDAO>(entity =>
            {
                entity.ToTable("EmployeePosition", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_UserGroup")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.EmployeeDetail)
                    .WithMany(p => p.EmployeePositions)
                    .HasForeignKey(d => d.EmployeeDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeePosition_EmployeeDetail");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.EmployeePositions)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeGroup_Group");
            });

            modelBuilder.Entity<Employee_HROrganizationDAO>(entity =>
            {
                entity.HasKey(e => new { e.HROrganizationId, e.EmployeeId })
                    .HasName("PK_EmployeeHROrganization")
                    .HasAnnotation("SqlServer:Clustered", false);

                entity.ToTable("Employee_HROrganization", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_EmployeeHROrganization")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Employee_HROrganizations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_HROrganization_Employee");

                entity.HasOne(d => d.HROrganization)
                    .WithMany(p => p.Employee_HROrganizations)
                    .HasForeignKey(d => d.HROrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_HROrganization_HROrganization");
            });

            modelBuilder.Entity<Employee_InventoryOrganizationAddressDAO>(entity =>
            {
                entity.HasKey(e => new { e.InventoryOrganizationAddressId, e.EmployeeId })
                    .HasAnnotation("SqlServer:Clustered", false);

                entity.ToTable("Employee_InventoryOrganizationAddress", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Employee_InventoryOrganizationAddress")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Employee_InventoryOrganizationAddresses)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_InventoryOrganizationAddress_Employee");

                entity.HasOne(d => d.InventoryOrganizationAddress)
                    .WithMany(p => p.Employee_InventoryOrganizationAddresses)
                    .HasForeignKey(d => d.InventoryOrganizationAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_InventoryOrganizationAddress_InventoryOrganizationAddress");
            });

            modelBuilder.Entity<Employee_ProjectOrganizationDAO>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.ProjectOrganizationId })
                    .HasAnnotation("SqlServer:Clustered", false);

                entity.ToTable("Employee_ProjectOrganization", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("IX_Employee_ProjectOrganization")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Employee_ProjectOrganizations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_ProjectOrganization_Employee");

                entity.HasOne(d => d.ProjectOrganization)
                    .WithMany(p => p.Employee_ProjectOrganizations)
                    .HasForeignKey(d => d.ProjectOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_ProjectOrganization_ProjectOrganization");
            });

            modelBuilder.Entity<EnumMasterDataDAO>(entity =>
            {
                entity.ToTable("EnumMasterData", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_EnumMasterData")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<FeatureDAO>(entity =>
            {
                entity.ToTable("Feature", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Feature")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(3000);
            });

            modelBuilder.Entity<FeatureOperationDAO>(entity =>
            {
                entity.ToTable("FeatureOperation", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_FeatureOperation")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.FeatureOperations)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeatureOperation_Feature");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.FeatureOperations)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeatureOperation_Operation");
            });

            modelBuilder.Entity<FeaturePermissionDAO>(entity =>
            {
                entity.ToTable("FeaturePermission", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_FeaturePermission")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.FeaturePermissions)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeaturePermission_Feature");
            });

            modelBuilder.Entity<FiscalYearDAO>(entity =>
            {
                entity.ToTable("FiscalYear", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_FiscalCalendar")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryValuationMethod)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.FiscalYears)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FiscalCalendar_SetofBook");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.FiscalYears)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FiscalYear_EnumMasterData");
            });

            modelBuilder.Entity<GeneralPriceRateDAO>(entity =>
            {
                entity.ToTable("GeneralPriceRate", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_GeneralPriceRate")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<HROrganizationDAO>(entity =>
            {
                entity.ToTable("HROrganization", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Department")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.DivisionId })
                    .HasName("IX_Code_Department")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ShortName).HasMaxLength(100);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.HROrganizations)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Division");
            });

            modelBuilder.Entity<InventoryOrganizationDAO>(entity =>
            {
                entity.ToTable("InventoryOrganization", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_InventoryOrganization")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.DivisionId })
                    .HasName("IX_Code_InventoryOrganization")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.InventoryOrganizations)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryOrganization_Division");
            });

            modelBuilder.Entity<InventoryOrganizationAddressDAO>(entity =>
            {
                entity.ToTable("InventoryOrganizationAddress", "APPS");

                entity.HasIndex(e => e.Id)
                    .HasName("CX_AssetOrganizationAddress")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.InventoryOrganization)
                    .WithMany(p => p.InventoryOrganizationAddresses)
                    .HasForeignKey(d => d.InventoryOrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryOrganizationAddress_InventoryOrganization");
            });

            modelBuilder.Entity<ItemDAO>(entity =>
            {
                entity.ToTable("Item", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Item")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.BusinessGroupId })
                    .HasName("IX_Item")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CodeFromMarket).HasMaxLength(50);

                entity.Property(e => e.CodeFromSupplier).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_BusinessGroup");

                entity.HasOne(d => d.Characteristic)
                    .WithMany(p => p.ItemCharacteristics)
                    .HasForeignKey(d => d.CharacteristicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_EnumMasterData");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ItemStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_EnumMasterData1");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_UnitOfMeasure");
            });

            modelBuilder.Entity<ItemDetailDAO>(entity =>
            {
                entity.ToTable("ItemDetail", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_ItemDetail")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.DefaultValue).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemDetail_Item");

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.ItemDetails)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemDetail_LegalEntity");
            });

            modelBuilder.Entity<ItemDetail_ItemGroupingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ItemDetaiId, e.ItemGroupingId })
                    .HasName("PK_Item_ItemGrouping_1")
                    .HasAnnotation("SqlServer:Clustered", false);

                entity.ToTable("ItemDetail_ItemGrouping", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("IX_Item_ItemGrouping")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ItemDetai)
                    .WithMany(p => p.ItemDetail_ItemGroupings)
                    .HasForeignKey(d => d.ItemDetaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_ItemGrouping_ItemDetail");

                entity.HasOne(d => d.ItemGrouping)
                    .WithMany(p => p.ItemDetail_ItemGroupings)
                    .HasForeignKey(d => d.ItemGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_ItemGrouping_ItemGrouping");
            });

            modelBuilder.Entity<ItemDiscountDAO>(entity =>
            {
                entity.ToTable("ItemDiscount", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.DiscountType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.ItemDetail)
                    .WithMany(p => p.ItemDiscounts)
                    .HasForeignKey(d => d.ItemDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemDiscount_ItemDetail");
            });

            modelBuilder.Entity<ItemGroupingDAO>(entity =>
            {
                entity.ToTable("ItemGrouping", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_ItemGrouping")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.ItemGroupings)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemGrouping_LegalEntity");
            });

            modelBuilder.Entity<ItemMaterialDAO>(entity =>
            {
                entity.ToTable("ItemMaterial", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.ItemDetail)
                    .WithMany(p => p.ItemMaterials)
                    .HasForeignKey(d => d.ItemDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMaterial_ItemDetail");

                entity.HasOne(d => d.SourceItem)
                    .WithMany(p => p.ItemMaterials)
                    .HasForeignKey(d => d.SourceItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMaterial_Item");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.ItemMaterials)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMaterial_UnitOfMeasure");
            });

            modelBuilder.Entity<JobLevelDAO>(entity =>
            {
                entity.ToTable("JobLevel", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_JobLevel")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.JobLevels)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobLevel_BusinessGroup");
            });

            modelBuilder.Entity<JobTitleDAO>(entity =>
            {
                entity.ToTable("JobTitle", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_JobTitle")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<LegalEntityDAO>(entity =>
            {
                entity.ToTable("LegalEntity", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_LegalEntity")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.SetOfBookId })
                    .HasName("IX_Code_LegalEntity")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.ShortName).HasMaxLength(500);

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.LegalEntities)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LegalEntity_SetofBook");
            });

            modelBuilder.Entity<NotificationDAO>(entity =>
            {
                entity.ToTable("Notification", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Notification")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => e.Time)
                    .HasName("IX_Time_Notification");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.URL).HasColumnType("ntext");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<OperationDAO>(entity =>
            {
                entity.ToTable("Operation", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Operation")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentMethodDAO>(entity =>
            {
                entity.ToTable("PaymentMethod", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_PaymentMethod")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.SetOfBookId })
                    .HasName("IX_Code_PaymentMethod")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.PaymentMethods)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentMethod_SetofBook");
            });

            modelBuilder.Entity<PaymentTermDAO>(entity =>
            {
                entity.ToTable("PaymentTerm", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_PaymentTerm")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.PaymentTerms)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentTerm_SetofBook");
            });

            modelBuilder.Entity<PositionDAO>(entity =>
            {
                entity.ToTable("Position", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Group")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Position_LegalEntity");
            });

            modelBuilder.Entity<ProjectOrganizationDAO>(entity =>
            {
                entity.ToTable("ProjectOrganization", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_ProjectOrganization")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.DivisionId })
                    .HasName("IX_Code_ProjectOrganization")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.ProjectOrganizations)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectOrganization_Division");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ProjectOrganizations)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectOrganization_Employee");
            });

            modelBuilder.Entity<ProvinceDAO>(entity =>
            {
                entity.ToTable("Province", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<SetOfBookDAO>(entity =>
            {
                entity.ToTable("SetOfBook", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_SetofBook")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => new { e.Code, e.BusinessGroupId })
                    .HasName("IX_Code_SetOfBook")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.SetOfBooks)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SetofBook_BusinessGroup");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.SetOfBooks)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SetOfBook_Currency");
            });

            modelBuilder.Entity<SupplierDAO>(entity =>
            {
                entity.ToTable("Supplier", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Supplier")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.TaxCode).HasMaxLength(50);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_BusinessGroup");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_EnumMasterData");
            });

            modelBuilder.Entity<SupplierBankAccountDAO>(entity =>
            {
                entity.ToTable("SupplierBankAccount", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Branch)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.SupplierBankAccounts)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierBankAccount_Bank");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.SupplierBankAccounts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierBankAccount_Province");

                entity.HasOne(d => d.SupplierDetail)
                    .WithMany(p => p.SupplierBankAccounts)
                    .HasForeignKey(d => d.SupplierDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierBankAccount_SupplierDetail");
            });

            modelBuilder.Entity<SupplierContactDAO>(entity =>
            {
                entity.ToTable("SupplierContact", "APPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_SupplierContact_Province");

                entity.HasOne(d => d.SupplierDetail)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.SupplierDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierContact_SupplierDetail");
            });

            modelBuilder.Entity<SupplierDetailDAO>(entity =>
            {
                entity.ToTable("SupplierDetail", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_SupplierDetail")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.DebtLoad).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.SupplierDetails)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierDetail_LegalEntity");

                entity.HasOne(d => d.PaymentTerm)
                    .WithMany(p => p.SupplierDetails)
                    .HasForeignKey(d => d.PaymentTermId)
                    .HasConstraintName("FK_SupplierDetail_PaymentTerm");

                entity.HasOne(d => d.StaffInCharge)
                    .WithMany(p => p.SupplierDetails)
                    .HasForeignKey(d => d.StaffInChargeId)
                    .HasConstraintName("FK_SupplierDetail_Employee");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierDetails)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierDetail_Supplier");
            });

            modelBuilder.Entity<SupplierDetail_SupplierGroupingDAO>(entity =>
            {
                entity.ToTable("SupplierDetail_SupplierGrouping", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Supplier_SupplierGrouping")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.HasOne(d => d.SupplierDetail)
                    .WithMany(p => p.SupplierDetail_SupplierGroupings)
                    .HasForeignKey(d => d.SupplierDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierDetail_SupplierGrouping_SupplierDetail");

                entity.HasOne(d => d.SupplierGrouping)
                    .WithMany(p => p.SupplierDetail_SupplierGroupings)
                    .HasForeignKey(d => d.SupplierGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_SupplierGrouping_SupplierGrouping");
            });

            modelBuilder.Entity<SupplierGroupingDAO>(entity =>
            {
                entity.ToTable("SupplierGrouping", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_SupplierGrouping")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.LegalEntity)
                    .WithMany(p => p.SupplierGroupings)
                    .HasForeignKey(d => d.LegalEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierGroup_LegalEntity");
            });

            modelBuilder.Entity<SystemConfigurationDAO>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("SystemConfiguration", "APPSYS");

                entity.Property(e => e.Key)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<TaxDAO>(entity =>
            {
                entity.ToTable("Tax", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Tax")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Tax_Tax");

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tax_SetofBook1");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .HasConstraintName("FK_Tax_UnitOfMeasure");
            });

            modelBuilder.Entity<TaxTemplateDAO>(entity =>
            {
                entity.ToTable("TaxTemplate", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_TaxTemplate")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.TaxTemplates)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxTemplate_BusinessGroup");
            });

            modelBuilder.Entity<TaxTemplateDetailDAO>(entity =>
            {
                entity.ToTable("TaxTemplateDetail", "APPS");

                entity.HasIndex(e => e.Id)
                    .HasName("CX_TaxTemplateDetail")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.TaxTemplateDetails)
                    .HasForeignKey(d => d.TaxTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxTemplateDetail_TaxTemplate");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.TaxTemplateDetails)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxTemplateDetail_UnitOfMeasure");
            });

            modelBuilder.Entity<TransformationUnitDAO>(entity =>
            {
                entity.ToTable("TransformationUnit", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_TransformationUnit")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.PrimaryPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.BaseUnit)
                    .WithMany(p => p.TransformationUnits)
                    .HasForeignKey(d => d.BaseUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransformationUnit_UnitOfMeasure");

                entity.HasOne(d => d.ItemDetail)
                    .WithMany(p => p.TransformationUnits)
                    .HasForeignKey(d => d.ItemDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransformationUnit_ItemDetail");
            });

            modelBuilder.Entity<UnitOfMeasureDAO>(entity =>
            {
                entity.ToTable("UnitOfMeasure", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_UnitOfMeasure")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BusinessGroup)
                    .WithMany(p => p.UnitOfMeasures)
                    .HasForeignKey(d => d.BusinessGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitOfMeasure_BusinessGroup");
            });

            modelBuilder.Entity<UserDAO>(entity =>
            {
                entity.ToTable("User", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_User")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salt).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_User_Employee");
            });

            modelBuilder.Entity<UserProfileDAO>(entity =>
            {
                entity.ToTable("UserProfile", "APPSYS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_UserProfile")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_User");
            });

            modelBuilder.Entity<VoucherDAO>(entity =>
            {
                entity.ToTable("Voucher", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_Voucher")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.CreditAccount)
                    .WithMany(p => p.VoucherCreditAccounts)
                    .HasForeignKey(d => d.CreditAccountId)
                    .HasConstraintName("FK_Voucher_ChartOfAccount_Credit");

                entity.HasOne(d => d.DebitAccount)
                    .WithMany(p => p.VoucherDebitAccounts)
                    .HasForeignKey(d => d.DebitAccountId)
                    .HasConstraintName("FK_Voucher_ChartOfAccount_Debit");

                entity.HasOne(d => d.SetOfBook)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.SetOfBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_SetOfBook");

                entity.HasOne(d => d.VoucherType)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.VoucherTypeId)
                    .HasConstraintName("FK_Voucher_VoucherType");
            });

            modelBuilder.Entity<VoucherTypeDAO>(entity =>
            {
                entity.ToTable("VoucherType", "APPS");

                entity.HasIndex(e => e.CX)
                    .HasName("CX_VoucherType")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CX).ValueGeneratedOnAdd();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingExt(modelBuilder);
        }

        partial void OnModelCreatingExt(ModelBuilder modelBuilder);
    }
}

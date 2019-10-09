using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class EnumMasterDataDAO
    {
        public EnumMasterDataDAO()
        {
            AccountingPeriods = new HashSet<AccountingPeriodDAO>();
            AssetStatuses = new HashSet<AssetDAO>();
            AssetTypes = new HashSet<AssetDAO>();
            ChartOfAccounts = new HashSet<ChartOfAccountDAO>();
            Customers = new HashSet<CustomerDAO>();
            Employees = new HashSet<EmployeeDAO>();
            FiscalYears = new HashSet<FiscalYearDAO>();
            ItemCharacteristics = new HashSet<ItemDAO>();
            ItemStatuses = new HashSet<ItemDAO>();
            Suppliers = new HashSet<SupplierDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual ICollection<AccountingPeriodDAO> AccountingPeriods { get; set; }
        public virtual ICollection<AssetDAO> AssetStatuses { get; set; }
        public virtual ICollection<AssetDAO> AssetTypes { get; set; }
        public virtual ICollection<ChartOfAccountDAO> ChartOfAccounts { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
        public virtual ICollection<FiscalYearDAO> FiscalYears { get; set; }
        public virtual ICollection<ItemDAO> ItemCharacteristics { get; set; }
        public virtual ICollection<ItemDAO> ItemStatuses { get; set; }
        public virtual ICollection<SupplierDAO> Suppliers { get; set; }
    }
}

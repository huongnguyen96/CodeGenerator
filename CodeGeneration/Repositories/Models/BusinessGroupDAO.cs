using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class BusinessGroupDAO
    {
        public BusinessGroupDAO()
        {
            APPSPermissions = new HashSet<APPSPermissionDAO>();
            Assets = new HashSet<AssetDAO>();
            Banks = new HashSet<BankDAO>();
            COATemplates = new HashSet<COATemplateDAO>();
            Currencies = new HashSet<CurrencyDAO>();
            Customers = new HashSet<CustomerDAO>();
            Employees = new HashSet<EmployeeDAO>();
            Items = new HashSet<ItemDAO>();
            JobLevels = new HashSet<JobLevelDAO>();
            SetOfBooks = new HashSet<SetOfBookDAO>();
            Suppliers = new HashSet<SupplierDAO>();
            TaxTemplates = new HashSet<TaxTemplateDAO>();
            UnitOfMeasures = new HashSet<UnitOfMeasureDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<APPSPermissionDAO> APPSPermissions { get; set; }
        public virtual ICollection<AssetDAO> Assets { get; set; }
        public virtual ICollection<BankDAO> Banks { get; set; }
        public virtual ICollection<COATemplateDAO> COATemplates { get; set; }
        public virtual ICollection<CurrencyDAO> Currencies { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
        public virtual ICollection<ItemDAO> Items { get; set; }
        public virtual ICollection<JobLevelDAO> JobLevels { get; set; }
        public virtual ICollection<SetOfBookDAO> SetOfBooks { get; set; }
        public virtual ICollection<SupplierDAO> Suppliers { get; set; }
        public virtual ICollection<TaxTemplateDAO> TaxTemplates { get; set; }
        public virtual ICollection<UnitOfMeasureDAO> UnitOfMeasures { get; set; }
    }
}

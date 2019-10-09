using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class SetOfBookDAO
    {
        public SetOfBookDAO()
        {
            APPSPermissions = new HashSet<APPSPermissionDAO>();
            BankAccounts = new HashSet<BankAccountDAO>();
            ChartOfAccounts = new HashSet<ChartOfAccountDAO>();
            CostCenters = new HashSet<CostCenterDAO>();
            FiscalYears = new HashSet<FiscalYearDAO>();
            LegalEntities = new HashSet<LegalEntityDAO>();
            PaymentMethods = new HashSet<PaymentMethodDAO>();
            PaymentTerms = new HashSet<PaymentTermDAO>();
            Taxes = new HashSet<TaxDAO>();
            Vouchers = new HashSet<VoucherDAO>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid CurrencyId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid? ChartOfAccountTemplateId { get; set; }
        public Guid? EnvironmentTaxTemplateId { get; set; }
        public Guid? ExportTaxTemplateId { get; set; }
        public Guid? ImportTaxTemplateId { get; set; }
        public Guid? NaturalResourceTaxTemplateId { get; set; }
        public Guid? SpecialConsumptionTaxTemplateId { get; set; }
        public Guid? ValueAddedTaxTemplateId { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual ICollection<APPSPermissionDAO> APPSPermissions { get; set; }
        public virtual ICollection<BankAccountDAO> BankAccounts { get; set; }
        public virtual ICollection<ChartOfAccountDAO> ChartOfAccounts { get; set; }
        public virtual ICollection<CostCenterDAO> CostCenters { get; set; }
        public virtual ICollection<FiscalYearDAO> FiscalYears { get; set; }
        public virtual ICollection<LegalEntityDAO> LegalEntities { get; set; }
        public virtual ICollection<PaymentMethodDAO> PaymentMethods { get; set; }
        public virtual ICollection<PaymentTermDAO> PaymentTerms { get; set; }
        public virtual ICollection<TaxDAO> Taxes { get; set; }
        public virtual ICollection<VoucherDAO> Vouchers { get; set; }
    }
}

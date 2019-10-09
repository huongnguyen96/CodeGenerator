using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ChartOfAccountDAO
    {
        public ChartOfAccountDAO()
        {
            BankAccounts = new HashSet<BankAccountDAO>();
            CostCenters = new HashSet<CostCenterDAO>();
            InverseParent = new HashSet<ChartOfAccountDAO>();
            VoucherCreditAccounts = new HashSet<VoucherDAO>();
            VoucherDebitAccounts = new HashSet<VoucherDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid SetOfBookId { get; set; }
        public Guid? ParentId { get; set; }
        public string AccountCode { get; set; }
        public string AliasCode { get; set; }
        public string AccountName { get; set; }
        public string AccountDescription { get; set; }
        public Guid? Characteristic { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual EnumMasterDataDAO CharacteristicNavigation { get; set; }
        public virtual ChartOfAccountDAO Parent { get; set; }
        public virtual SetOfBookDAO SetOfBook { get; set; }
        public virtual ICollection<BankAccountDAO> BankAccounts { get; set; }
        public virtual ICollection<CostCenterDAO> CostCenters { get; set; }
        public virtual ICollection<ChartOfAccountDAO> InverseParent { get; set; }
        public virtual ICollection<VoucherDAO> VoucherCreditAccounts { get; set; }
        public virtual ICollection<VoucherDAO> VoucherDebitAccounts { get; set; }
    }
}

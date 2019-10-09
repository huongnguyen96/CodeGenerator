using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class SupplierDetailDAO
    {
        public SupplierDetailDAO()
        {
            SupplierBankAccounts = new HashSet<SupplierBankAccountDAO>();
            SupplierContacts = new HashSet<SupplierContactDAO>();
            SupplierDetail_SupplierGroupings = new HashSet<SupplierDetail_SupplierGroupingDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid LegalEntityId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid? StaffInChargeId { get; set; }
        public Guid? PaymentTermId { get; set; }
        public decimal? DebtLoad { get; set; }
        public int? DueInDays { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual PaymentTermDAO PaymentTerm { get; set; }
        public virtual EmployeeDAO StaffInCharge { get; set; }
        public virtual SupplierDAO Supplier { get; set; }
        public virtual ICollection<SupplierBankAccountDAO> SupplierBankAccounts { get; set; }
        public virtual ICollection<SupplierContactDAO> SupplierContacts { get; set; }
        public virtual ICollection<SupplierDetail_SupplierGroupingDAO> SupplierDetail_SupplierGroupings { get; set; }
    }
}

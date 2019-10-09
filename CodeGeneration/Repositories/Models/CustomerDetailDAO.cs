using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerDetailDAO
    {
        public CustomerDetailDAO()
        {
            CustomerBankAccounts = new HashSet<CustomerBankAccountDAO>();
            CustomerContacts = new HashSet<CustomerContactDAO>();
            CustomerDetail_CustomerGroupings = new HashSet<CustomerDetail_CustomerGroupingDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid LegalEntityId { get; set; }
        public Guid? PaymentTermId { get; set; }
        public int? DueInDays { get; set; }
        public decimal? DebtLoad { get; set; }
        public Guid? StaffInChargeId { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual PaymentTermDAO PaymentTerm { get; set; }
        public virtual EmployeeDAO StaffInCharge { get; set; }
        public virtual ICollection<CustomerBankAccountDAO> CustomerBankAccounts { get; set; }
        public virtual ICollection<CustomerContactDAO> CustomerContacts { get; set; }
        public virtual ICollection<CustomerDetail_CustomerGroupingDAO> CustomerDetail_CustomerGroupings { get; set; }
    }
}

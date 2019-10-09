using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class BankDAO
    {
        public BankDAO()
        {
            BankAccounts = new HashSet<BankAccountDAO>();
            CustomerBankAccounts = new HashSet<CustomerBankAccountDAO>();
            EmployeeDetails = new HashSet<EmployeeDetailDAO>();
            SupplierBankAccounts = new HashSet<SupplierBankAccountDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid BusinessGroupId { get; set; }
        public string Description { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual ICollection<BankAccountDAO> BankAccounts { get; set; }
        public virtual ICollection<CustomerBankAccountDAO> CustomerBankAccounts { get; set; }
        public virtual ICollection<EmployeeDetailDAO> EmployeeDetails { get; set; }
        public virtual ICollection<SupplierBankAccountDAO> SupplierBankAccounts { get; set; }
    }
}

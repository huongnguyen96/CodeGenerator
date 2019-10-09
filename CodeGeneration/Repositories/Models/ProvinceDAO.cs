using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ProvinceDAO
    {
        public ProvinceDAO()
        {
            CustomerBankAccounts = new HashSet<CustomerBankAccountDAO>();
            CustomerContacts = new HashSet<CustomerContactDAO>();
            EmployeeDetails = new HashSet<EmployeeDetailDAO>();
            SupplierBankAccounts = new HashSet<SupplierBankAccountDAO>();
            SupplierContacts = new HashSet<SupplierContactDAO>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public long CX { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual ICollection<CustomerBankAccountDAO> CustomerBankAccounts { get; set; }
        public virtual ICollection<CustomerContactDAO> CustomerContacts { get; set; }
        public virtual ICollection<EmployeeDetailDAO> EmployeeDetails { get; set; }
        public virtual ICollection<SupplierBankAccountDAO> SupplierBankAccounts { get; set; }
        public virtual ICollection<SupplierContactDAO> SupplierContacts { get; set; }
    }
}

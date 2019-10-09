using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerBankAccountDAO
    {
        public Guid Id { get; set; }
        public Guid CustomerDetailId { get; set; }
        public long CX { get; set; }
        public string Name { get; set; }
        public Guid BankId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Branch { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual BankDAO Bank { get; set; }
        public virtual CustomerDetailDAO CustomerDetail { get; set; }
        public virtual ProvinceDAO Province { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class PaymentTermDAO
    {
        public PaymentTermDAO()
        {
            CustomerDetails = new HashSet<CustomerDetailDAO>();
            SupplierDetails = new HashSet<SupplierDetailDAO>();
        }

        public Guid Id { get; set; }
        public Guid SetOfBookId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DueInDays { get; set; }
        public int? DiscountPeriod { get; set; }
        public bool Disabled { get; set; }
        public long CX { get; set; }
        public double? DiscountRate { get; set; }
        public int Sequence { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual SetOfBookDAO SetOfBook { get; set; }
        public virtual ICollection<CustomerDetailDAO> CustomerDetails { get; set; }
        public virtual ICollection<SupplierDetailDAO> SupplierDetails { get; set; }
    }
}

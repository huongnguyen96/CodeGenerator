using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerDAO
    {
        public CustomerDAO()
        {
            CustomerDetails = new HashSet<CustomerDetailDAO>();
        }

        public Guid Id { get; set; }
        public Guid BusinessGroupId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public string Note { get; set; }

        public virtual BusinessGroupDAO BusinessGroup { get; set; }
        public virtual EnumMasterDataDAO Status { get; set; }
        public virtual ICollection<CustomerDetailDAO> CustomerDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerGroupingDAO
    {
        public CustomerGroupingDAO()
        {
            CustomerDetail_CustomerGroupings = new HashSet<CustomerDetail_CustomerGroupingDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid LegalEntityId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual ICollection<CustomerDetail_CustomerGroupingDAO> CustomerDetail_CustomerGroupings { get; set; }
    }
}

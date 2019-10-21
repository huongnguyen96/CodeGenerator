using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Customer_CustomerGroupingDAO
    {
        public long CustomerId { get; set; }
        public long CustomerGroupingId { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual CustomerGroupingDAO CustomerGrouping { get; set; }
    }
}

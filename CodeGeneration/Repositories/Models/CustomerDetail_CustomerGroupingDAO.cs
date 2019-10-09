using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class CustomerDetail_CustomerGroupingDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid CustomerDetailId { get; set; }
        public Guid CustomerGroupingId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual CustomerDetailDAO CustomerDetail { get; set; }
        public virtual CustomerGroupingDAO CustomerGrouping { get; set; }
    }
}

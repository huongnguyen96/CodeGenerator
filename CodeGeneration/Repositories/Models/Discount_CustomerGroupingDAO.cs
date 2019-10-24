using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class Discount_CustomerGroupingDAO
    {
        public long DiscountId { get; set; }
        public long CustomerGroupingId { get; set; }

        public virtual CustomerGroupingDAO CustomerGrouping { get; set; }
        public virtual DiscountDAO Discount { get; set; }
    }
}

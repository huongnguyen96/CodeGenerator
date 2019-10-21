using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class DiscountCustomerGroupingDAO
    {
        public long Id { get; set; }
        public long DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }

        public virtual DiscountDAO Discount { get; set; }
    }
}

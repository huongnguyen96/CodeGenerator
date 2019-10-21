using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class DiscountItemDAO
    {
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }

        public virtual DiscountDAO Discount { get; set; }
        public virtual UnitDAO Unit { get; set; }
    }
}

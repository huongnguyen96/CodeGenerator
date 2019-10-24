using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class DiscountContentDAO
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }

        public virtual DiscountDAO Discount { get; set; }
        public virtual ItemDAO Item { get; set; }
    }
}

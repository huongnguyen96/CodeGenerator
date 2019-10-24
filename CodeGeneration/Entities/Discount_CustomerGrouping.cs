
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Discount_CustomerGrouping : DataEntity
    {
        
        public long DiscountId { get; set; }
        public long CustomerGroupingId { get; set; }
        public CustomerGrouping CustomerGrouping { get; set; }
        public Discount Discount { get; set; }
    }

    public class Discount_CustomerGroupingFilter : FilterEntity
    {
        
        public LongFilter DiscountId { get; set; }
        public LongFilter CustomerGroupingId { get; set; }
        public Discount_CustomerGroupingOrder OrderBy {get; set;}
        public Discount_CustomerGroupingSelect Selects {get; set;}
    }

    public enum Discount_CustomerGroupingOrder
    {
        
        Discount = 1,
        CustomerGrouping = 2,
    }
    
    [Flags]
    public enum Discount_CustomerGroupingSelect:long
    {
        ALL = E.ALL,
        
        Discount = E._1,
        CustomerGrouping = E._2,
    }
}

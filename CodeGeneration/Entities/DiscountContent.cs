
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class DiscountContent : DataEntity
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public Discount Discount { get; set; }
        public Item Item { get; set; }
    }

    public class DiscountContentFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter ItemId { get; set; }
        public LongFilter DiscountValue { get; set; }
        public LongFilter DiscountId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public DiscountContentOrder OrderBy {get; set;}
        public DiscountContentSelect Selects {get; set;}
    }

    public enum DiscountContentOrder
    {
        
        Id = 1,
        Item = 2,
        DiscountValue = 3,
        Discount = 4,
    }
    
    [Flags]
    public enum DiscountContentSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Item = E._2,
        DiscountValue = E._3,
        Discount = E._4,
    }
}

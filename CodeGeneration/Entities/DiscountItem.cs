
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class DiscountItem : DataEntity
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public Discount Discount { get; set; }
        public Unit Unit { get; set; }
    }

    public class DiscountItemFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter UnitId { get; set; }
        public LongFilter DiscountValue { get; set; }
        public LongFilter DiscountId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public DiscountItemOrder OrderBy {get; set;}
        public DiscountItemSelect Selects {get; set;}
    }

    public enum DiscountItemOrder
    {
        
        Id = 1,
        Unit = 2,
        DiscountValue = 3,
        Discount = 4,
    }

    public enum DiscountItemSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Unit = E._2,
        DiscountValue = E._3,
        Discount = E._4,
    }
}

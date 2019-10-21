
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class DiscountCustomerGrouping : DataEntity
    {
        
        public long Id { get; set; }
        public long DiscountId { get; set; }
        public string CustomerGroupingCode { get; set; }
        public Discount Discount { get; set; }
    }

    public class DiscountCustomerGroupingFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter DiscountId { get; set; }
        public StringFilter CustomerGroupingCode { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public DiscountCustomerGroupingOrder OrderBy {get; set;}
        public DiscountCustomerGroupingSelect Selects {get; set;}
    }

    public enum DiscountCustomerGroupingOrder
    {
        
        Id = 1,
        Discount = 2,
        CustomerGroupingCode = 3,
    }

    public enum DiscountCustomerGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Discount = E._2,
        CustomerGroupingCode = E._3,
    }
}

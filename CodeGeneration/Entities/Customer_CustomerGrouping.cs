
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Customer_CustomerGrouping : DataEntity
    {
        
        public long CustomerId { get; set; }
        public long CustomerGroupingId { get; set; }
        public Customer Customer { get; set; }
        public CustomerGrouping CustomerGrouping { get; set; }
    }

    public class Customer_CustomerGroupingFilter : FilterEntity
    {
        
        public LongFilter CustomerId { get; set; }
        public LongFilter CustomerGroupingId { get; set; }
        public Customer_CustomerGroupingOrder OrderBy {get; set;}
        public Customer_CustomerGroupingSelect Selects {get; set;}
    }

    public enum Customer_CustomerGroupingOrder
    {
        
        Customer = 1,
        CustomerGrouping = 2,
    }
    
    [Flags]
    public enum Customer_CustomerGroupingSelect:long
    {
        ALL = E.ALL,
        
        Customer = E._1,
        CustomerGrouping = E._2,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class CustomerGrouping : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Customer_CustomerGrouping> Customer_CustomerGroupings { get; set; }
        public List<Discount_CustomerGrouping> Discount_CustomerGroupings { get; set; }
    }

    public class CustomerGroupingFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public CustomerGroupingOrder OrderBy {get; set;}
        public CustomerGroupingSelect Selects {get; set;}
    }

    public enum CustomerGroupingOrder
    {
        
        Id = 1,
        Name = 2,
    }
    
    [Flags]
    public enum CustomerGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
    }
}

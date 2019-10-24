
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Discount : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
        public List<DiscountContent> DiscountContents { get; set; }
        public List<Discount_CustomerGrouping> Discount_CustomerGroupings { get; set; }
    }

    public class DiscountFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public DateTimeFilter Start { get; set; }
        public DateTimeFilter End { get; set; }
        public StringFilter Type { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public DiscountOrder OrderBy {get; set;}
        public DiscountSelect Selects {get; set;}
    }

    public enum DiscountOrder
    {
        
        Id = 1,
        Name = 2,
        Start = 3,
        End = 4,
        Type = 5,
    }
    
    [Flags]
    public enum DiscountSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Start = E._3,
        End = E._4,
        Type = E._5,
    }
}

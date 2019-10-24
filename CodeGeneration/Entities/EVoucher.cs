
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class EVoucher : DataEntity
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long Quantity { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public List<EVoucherContent> EVoucherContents { get; set; }
    }

    public class EVoucherFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter CustomerId { get; set; }
        public LongFilter ProductId { get; set; }
        public StringFilter Name { get; set; }
        public DateTimeFilter Start { get; set; }
        public DateTimeFilter End { get; set; }
        public LongFilter Quantity { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public EVoucherOrder OrderBy {get; set;}
        public EVoucherSelect Selects {get; set;}
    }

    public enum EVoucherOrder
    {
        
        Id = 1,
        Customer = 2,
        Product = 3,
        Name = 4,
        Start = 5,
        End = 6,
        Quantity = 7,
    }
    
    [Flags]
    public enum EVoucherSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Customer = E._2,
        Product = E._3,
        Name = E._4,
        Start = E._5,
        End = E._6,
        Quantity = E._7,
    }
}

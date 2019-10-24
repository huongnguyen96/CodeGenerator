
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class OrderContent : DataEntity
    {
        
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long? ItemId { get; set; }
        public string ProductName { get; set; }
        public string FirstVersion { get; set; }
        public string SecondVersion { get; set; }
        public long Price { get; set; }
        public long DiscountPrice { get; set; }
        public long Quantity { get; set; }
        public Item Item { get; set; }
        public Order Order { get; set; }
    }

    public class OrderContentFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter OrderId { get; set; }
        public LongFilter ItemId { get; set; }
        public StringFilter ProductName { get; set; }
        public StringFilter FirstVersion { get; set; }
        public StringFilter SecondVersion { get; set; }
        public LongFilter Price { get; set; }
        public LongFilter DiscountPrice { get; set; }
        public LongFilter Quantity { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public OrderContentOrder OrderBy {get; set;}
        public OrderContentSelect Selects {get; set;}
    }

    public enum OrderContentOrder
    {
        
        Id = 1,
        Order = 2,
        Item = 3,
        ProductName = 4,
        FirstVersion = 5,
        SecondVersion = 6,
        Price = 7,
        DiscountPrice = 8,
        Quantity = 9,
    }
    
    [Flags]
    public enum OrderContentSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Order = E._2,
        Item = E._3,
        ProductName = E._4,
        FirstVersion = E._5,
        SecondVersion = E._6,
        Price = E._7,
        DiscountPrice = E._8,
        Quantity = E._9,
    }
}

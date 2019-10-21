
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class OrderContent : DataEntity
    {
        
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string ItemName { get; set; }
        public string FirstVersion { get; set; }
        public string SecondVersion { get; set; }
        public string ThirdVersion { get; set; }
        public long Price { get; set; }
        public long DiscountPrice { get; set; }
        public Order Order { get; set; }
    }

    public class OrderContentFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter OrderId { get; set; }
        public StringFilter ItemName { get; set; }
        public StringFilter FirstVersion { get; set; }
        public StringFilter SecondVersion { get; set; }
        public StringFilter ThirdVersion { get; set; }
        public LongFilter Price { get; set; }
        public LongFilter DiscountPrice { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public OrderContentOrder OrderBy {get; set;}
        public OrderContentSelect Selects {get; set;}
    }

    public enum OrderContentOrder
    {
        
        Id = 1,
        Order = 2,
        ItemName = 3,
        FirstVersion = 4,
        SecondVersion = 5,
        ThirdVersion = 6,
        Price = 7,
        DiscountPrice = 8,
    }

    public enum OrderContentSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Order = E._2,
        ItemName = E._3,
        FirstVersion = E._4,
        SecondVersion = E._5,
        ThirdVersion = E._6,
        Price = E._7,
        DiscountPrice = E._8,
    }
}

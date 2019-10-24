
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class OrderStatus : DataEntity
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class OrderStatusFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public OrderStatusOrder OrderBy {get; set;}
        public OrderStatusSelect Selects {get; set;}
    }

    public enum OrderStatusOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
        Description = 4,
    }
    
    [Flags]
    public enum OrderStatusSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        Description = E._4,
    }
}

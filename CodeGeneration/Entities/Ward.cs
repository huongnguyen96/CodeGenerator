
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Ward : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long DistrictId { get; set; }
        public District District { get; set; }
        public List<ShippingAddress> ShippingAddresses { get; set; }
    }

    public class WardFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public LongFilter DistrictId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public WardOrder OrderBy {get; set;}
        public WardSelect Selects {get; set;}
    }

    public enum WardOrder
    {
        
        Id = 1,
        Name = 2,
        OrderNumber = 3,
        District = 4,
    }
    
    [Flags]
    public enum WardSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        OrderNumber = E._3,
        District = E._4,
    }
}

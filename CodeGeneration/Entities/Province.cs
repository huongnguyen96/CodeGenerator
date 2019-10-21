
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Province : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public List<District> Districts { get; set; }
        public List<ShippingAddress> ShippingAddresses { get; set; }
    }

    public class ProvinceFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ProvinceOrder OrderBy {get; set;}
        public ProvinceSelect Selects {get; set;}
    }

    public enum ProvinceOrder
    {
        
        Id = 1,
        Name = 2,
        OrderNumber = 3,
    }

    public enum ProvinceSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        OrderNumber = E._3,
    }
}

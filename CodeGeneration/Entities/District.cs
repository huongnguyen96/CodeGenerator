
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class District : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long ProvinceId { get; set; }
        public Province Province { get; set; }
        public List<ShippingAddress> ShippingAddresses { get; set; }
        public List<Ward> Wards { get; set; }
    }

    public class DistrictFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public LongFilter ProvinceId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public DistrictOrder OrderBy {get; set;}
        public DistrictSelect Selects {get; set;}
    }

    public enum DistrictOrder
    {
        
        Id = 1,
        Name = 2,
        OrderNumber = 3,
        Province = 4,
    }
    
    [Flags]
    public enum DistrictSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        OrderNumber = E._3,
        Province = E._4,
    }
}

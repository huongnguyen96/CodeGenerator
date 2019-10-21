
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class ShippingAddress : DataEntity
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long WardId { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public Customer Customer { get; set; }
        public District District { get; set; }
        public Province Province { get; set; }
        public Ward Ward { get; set; }
    }

    public class ShippingAddressFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter CustomerId { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter CompanyName { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public LongFilter ProvinceId { get; set; }
        public LongFilter DistrictId { get; set; }
        public LongFilter WardId { get; set; }
        public StringFilter Address { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ShippingAddressOrder OrderBy {get; set;}
        public ShippingAddressSelect Selects {get; set;}
    }

    public enum ShippingAddressOrder
    {
        
        Id = 1,
        Customer = 2,
        FullName = 3,
        CompanyName = 4,
        PhoneNumber = 5,
        Province = 6,
        District = 7,
        Ward = 8,
        Address = 9,
        IsDefault = 10,
    }

    public enum ShippingAddressSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Customer = E._2,
        FullName = E._3,
        CompanyName = E._4,
        PhoneNumber = E._5,
        Province = E._6,
        District = E._7,
        Ward = E._8,
        Address = E._9,
        IsDefault = E._10,
    }
}

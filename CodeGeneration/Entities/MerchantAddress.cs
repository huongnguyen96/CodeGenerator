
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class MerchantAddress : DataEntity
    {
        
        public long Id { get; set; }
        public long MerchantId { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public Merchant Merchant { get; set; }
        public List<Product_MerchantAddress> Product_MerchantAddresses { get; set; }
    }

    public class MerchantAddressFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter MerchantId { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter Contact { get; set; }
        public StringFilter Phone { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public MerchantAddressOrder OrderBy {get; set;}
        public MerchantAddressSelect Selects {get; set;}
    }

    public enum MerchantAddressOrder
    {
        
        Id = 1,
        Merchant = 2,
        Code = 3,
        Address = 4,
        Contact = 5,
        Phone = 6,
    }
    
    [Flags]
    public enum MerchantAddressSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Merchant = E._2,
        Code = E._3,
        Address = E._4,
        Contact = E._5,
        Phone = E._6,
    }
}

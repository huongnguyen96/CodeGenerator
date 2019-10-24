
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Merchant : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public List<MerchantAddress> MerchantAddresses { get; set; }
        public List<Product> Products { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }

    public class MerchantFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter ContactPerson { get; set; }
        public StringFilter Address { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public MerchantOrder OrderBy {get; set;}
        public MerchantSelect Selects {get; set;}
    }

    public enum MerchantOrder
    {
        
        Id = 1,
        Name = 2,
        Phone = 3,
        ContactPerson = 4,
        Address = 5,
    }
    
    [Flags]
    public enum MerchantSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Phone = E._3,
        ContactPerson = E._4,
        Address = E._5,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Product_MerchantAddress : DataEntity
    {
        
        public long ProductId { get; set; }
        public long MerchantAddressId { get; set; }
        public MerchantAddress MerchantAddress { get; set; }
        public Product Product { get; set; }
    }

    public class Product_MerchantAddressFilter : FilterEntity
    {
        
        public LongFilter ProductId { get; set; }
        public LongFilter MerchantAddressId { get; set; }
        public Product_MerchantAddressOrder OrderBy {get; set;}
        public Product_MerchantAddressSelect Selects {get; set;}
    }

    public enum Product_MerchantAddressOrder
    {
        
        Product = 1,
        MerchantAddress = 2,
    }
    
    [Flags]
    public enum Product_MerchantAddressSelect:long
    {
        ALL = E.ALL,
        
        Product = E._1,
        MerchantAddress = E._2,
    }
}

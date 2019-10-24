
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Product_PaymentMethod : DataEntity
    {
        
        public long ProductId { get; set; }
        public long PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Product Product { get; set; }
    }

    public class Product_PaymentMethodFilter : FilterEntity
    {
        
        public LongFilter ProductId { get; set; }
        public LongFilter PaymentMethodId { get; set; }
        public Product_PaymentMethodOrder OrderBy {get; set;}
        public Product_PaymentMethodSelect Selects {get; set;}
    }

    public enum Product_PaymentMethodOrder
    {
        
        Product = 1,
        PaymentMethod = 2,
    }
    
    [Flags]
    public enum Product_PaymentMethodSelect:long
    {
        ALL = E.ALL,
        
        Product = E._1,
        PaymentMethod = E._2,
    }
}

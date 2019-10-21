
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Customer : DataEntity
    {
        
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public List<Customer_CustomerGrouping> Customer_CustomerGroupings { get; set; }
        public List<Order> Orders { get; set; }
        public List<ShippingAddress> ShippingAddresses { get; set; }
    }

    public class CustomerFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter DisplayName { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public CustomerOrder OrderBy {get; set;}
        public CustomerSelect Selects {get; set;}
    }

    public enum CustomerOrder
    {
        
        Id = 1,
        Username = 2,
        DisplayName = 3,
    }

    public enum CustomerSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Username = E._2,
        DisplayName = E._3,
    }
}

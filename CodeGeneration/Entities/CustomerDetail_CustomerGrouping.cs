
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class CustomerDetail_CustomerGrouping : DataEntity
    {
        public Guid Id { get; set; }
		public Guid CustomerDetailId { get; set; }
		public Guid CustomerGroupingId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class CustomerDetail_CustomerGroupingFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter CustomerDetailId { get; set; }
		public GuidFilter CustomerGroupingId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public CustomerDetail_CustomerGroupingOrder OrderBy {get; set;}
        public CustomerDetail_CustomerGroupingSelect Selects {get; set;}
    }

    public enum CustomerDetail_CustomerGroupingOrder
    {
        
    }

    public enum CustomerDetail_CustomerGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        CustomerDetail = E._2,
        CustomerGrouping = E._3,
        BusinessGroup = E._4,
    }
}

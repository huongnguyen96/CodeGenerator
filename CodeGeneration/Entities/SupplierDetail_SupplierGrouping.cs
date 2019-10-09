
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class SupplierDetail_SupplierGrouping : DataEntity
    {
        public Guid Id { get; set; }
		public Guid SupplierGroupingId { get; set; }
		public Guid SupplierDetailId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class SupplierDetail_SupplierGroupingFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter SupplierGroupingId { get; set; }
		public GuidFilter SupplierDetailId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public SupplierDetail_SupplierGroupingOrder OrderBy {get; set;}
        public SupplierDetail_SupplierGroupingSelect Selects {get; set;}
    }

    public enum SupplierDetail_SupplierGroupingOrder
    {
        
    }

    public enum SupplierDetail_SupplierGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        SupplierGrouping = E._2,
        SupplierDetail = E._3,
        BusinessGroup = E._4,
    }
}

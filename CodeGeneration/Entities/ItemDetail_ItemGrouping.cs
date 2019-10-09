
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class ItemDetail_ItemGrouping : DataEntity
    {
        public Guid ItemDetaiId { get; set; }
		public Guid ItemGroupingId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class ItemDetail_ItemGroupingFilter : FilterEntity
    {
        public GuidFilter ItemDetaiId { get; set; }
		public GuidFilter ItemGroupingId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public ItemDetail_ItemGroupingOrder OrderBy {get; set;}
        public ItemDetail_ItemGroupingSelect Selects {get; set;}
    }

    public enum ItemDetail_ItemGroupingOrder
    {
        
    }

    public enum ItemDetail_ItemGroupingSelect:long
    {
        ALL = E.ALL,
        
        ItemDetai = E._1,
        ItemGrouping = E._2,
        BusinessGroup = E._3,
    }
}

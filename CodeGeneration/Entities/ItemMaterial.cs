
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class ItemMaterial : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid SourceItemId { get; set; }
		public Guid UnitOfMeasureId { get; set; }
		public Guid? Quantity { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid ItemDetailId { get; set; }
		
    }

    public class ItemMaterialFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter SourceItemId { get; set; }
		public GuidFilter UnitOfMeasureId { get; set; }
		public GuidFilter Quantity { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter ItemDetailId { get; set; }
		
        public ItemMaterialOrder OrderBy {get; set;}
        public ItemMaterialSelect Selects {get; set;}
    }

    public enum ItemMaterialOrder
    {
        
        Disabled,
        Quantity,
    }

    public enum ItemMaterialSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        SourceItem = E._3,
        UnitOfMeasure = E._4,
        Quantity = E._5,
        BusinessGroup = E._6,
        ItemDetail = E._7,
    }
}

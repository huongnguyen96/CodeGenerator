
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class ItemGrouping : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid LegalEntityId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		
    }

    public class ItemGroupingFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		
        public ItemGroupingOrder OrderBy {get; set;}
        public ItemGroupingSelect Selects {get; set;}
    }

    public enum ItemGroupingOrder
    {
        
        Disabled,
        Code,
        Name,
        Description,
    }

    public enum ItemGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        BusinessGroup = E._3,
        LegalEntity = E._4,
        Code = E._5,
        Name = E._6,
        Description = E._7,
    }
}

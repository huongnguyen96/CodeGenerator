
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class SupplierGrouping : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid LegalEntityId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class SupplierGroupingFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public SupplierGroupingOrder OrderBy {get; set;}
        public SupplierGroupingSelect Selects {get; set;}
    }

    public enum SupplierGroupingOrder
    {
        
        Disabled,
        Code,
        Name,
        Description,
    }

    public enum SupplierGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        Name = E._4,
        Description = E._5,
        LegalEntity = E._6,
        BusinessGroup = E._7,
    }
}

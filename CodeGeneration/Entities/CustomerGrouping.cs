
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class CustomerGrouping : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid LegalEntityId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class CustomerGroupingFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public CustomerGroupingOrder OrderBy {get; set;}
        public CustomerGroupingSelect Selects {get; set;}
    }

    public enum CustomerGroupingOrder
    {
        
        Disabled,
        Code,
        Name,
    }

    public enum CustomerGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        Name = E._4,
        LegalEntity = E._5,
        BusinessGroup = E._6,
    }
}

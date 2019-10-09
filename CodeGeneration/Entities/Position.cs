
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Position : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid LegalEntityId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class PositionFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public PositionOrder OrderBy {get; set;}
        public PositionSelect Selects {get; set;}
    }

    public enum PositionOrder
    {
        
        Disabled,
        Code,
        Name,
        Description,
    }

    public enum PositionSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        LegalEntity = E._3,
        Code = E._4,
        Name = E._5,
        Description = E._6,
        BusinessGroup = E._7,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class UnitOfMeasure : DataEntity
    {
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public Guid BusinessGroupId { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		
    }

    public class UnitOfMeasureFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Type { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Description { get; set; }
		
        public UnitOfMeasureOrder OrderBy {get; set;}
        public UnitOfMeasureSelect Selects {get; set;}
    }

    public enum UnitOfMeasureOrder
    {
        
        Name,
        Type,
        Disabled,
        Code,
        Description,
    }

    public enum UnitOfMeasureSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Type = E._3,
        BusinessGroup = E._4,
        Disabled = E._5,
        Code = E._6,
        Description = E._7,
    }
}

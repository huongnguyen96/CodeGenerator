
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Division : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid LegalEntityId { get; set; }
		public string Code { get; set; }
		public string ShortName { get; set; }
		public string Name { get; set; }
		public Guid BusinessGroupId { get; set; }
		public string Description { get; set; }
		
    }

    public class DivisionFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter ShortName { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public StringFilter Description { get; set; }
		
        public DivisionOrder OrderBy {get; set;}
        public DivisionSelect Selects {get; set;}
    }

    public enum DivisionOrder
    {
        
        Disabled,
        Code,
        ShortName,
        Name,
        Description,
    }

    public enum DivisionSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        LegalEntity = E._3,
        Code = E._4,
        ShortName = E._5,
        Name = E._6,
        BusinessGroup = E._7,
        Description = E._8,
    }
}

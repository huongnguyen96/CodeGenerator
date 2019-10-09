
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class LegalEntity : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid SetOfBookId { get; set; }
		public string Code { get; set; }
		public string ShortName { get; set; }
		public string Name { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class LegalEntityFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter ShortName { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public LegalEntityOrder OrderBy {get; set;}
        public LegalEntitySelect Selects {get; set;}
    }

    public enum LegalEntityOrder
    {
        
        Disabled,
        Code,
        ShortName,
        Name,
    }

    public enum LegalEntitySelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        SetOfBook = E._3,
        Code = E._4,
        ShortName = E._5,
        Name = E._6,
        BusinessGroup = E._7,
    }
}

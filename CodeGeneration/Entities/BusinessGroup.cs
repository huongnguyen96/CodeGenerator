
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class BusinessGroup : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string ShortName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		
    }

    public class BusinessGroupFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter ShortName { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		
        public BusinessGroupOrder OrderBy {get; set;}
        public BusinessGroupSelect Selects {get; set;}
    }

    public enum BusinessGroupOrder
    {
        
        Disabled,
        Code,
        ShortName,
        Name,
        Description,
    }

    public enum BusinessGroupSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        ShortName = E._4,
        Name = E._5,
        Description = E._6,
    }
}

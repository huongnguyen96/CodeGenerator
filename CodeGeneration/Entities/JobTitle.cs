
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class JobTitle : DataEntity
    {
        public Guid Id { get; set; }
		public Guid BusinessGroupId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public bool Disabled { get; set; }
		public string Description { get; set; }
		
    }

    public class JobTitleFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Description { get; set; }
		
        public JobTitleOrder OrderBy {get; set;}
        public JobTitleSelect Selects {get; set;}
    }

    public enum JobTitleOrder
    {
        
        Code,
        Name,
        Disabled,
        Description,
    }

    public enum JobTitleSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        BusinessGroup = E._2,
        Code = E._3,
        Name = E._4,
        Disabled = E._5,
        Description = E._6,
    }
}

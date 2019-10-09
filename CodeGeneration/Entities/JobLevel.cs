
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class JobLevel : DataEntity
    {
        public Guid Id { get; set; }
		public Guid BusinessGroupId { get; set; }
		public double Level { get; set; }
		public bool Disabled { get; set; }
		public string Description { get; set; }
		
    }

    public class JobLevelFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public DoubleFilter Level { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Description { get; set; }
		
        public JobLevelOrder OrderBy {get; set;}
        public JobLevelSelect Selects {get; set;}
    }

    public enum JobLevelOrder
    {
        
        Level,
        Disabled,
        Description,
    }

    public enum JobLevelSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        BusinessGroup = E._2,
        Level = E._3,
        Disabled = E._4,
        Description = E._5,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class COATemplate : DataEntity
    {
        public Guid Id { get; set; }
		public Guid BusinessGroupId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		
    }

    public class COATemplateFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Type { get; set; }
		
        public COATemplateOrder OrderBy {get; set;}
        public COATemplateSelect Selects {get; set;}
    }

    public enum COATemplateOrder
    {
        
        Name,
        Type,
    }

    public enum COATemplateSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        BusinessGroup = E._2,
        Name = E._3,
        Type = E._4,
    }
}

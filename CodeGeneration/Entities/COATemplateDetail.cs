
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class COATemplateDetail : DataEntity
    {
        public Guid Id { get; set; }
		public Guid COATemplateId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid? ParentId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class COATemplateDetailFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter COATemplateId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter ParentId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public COATemplateDetailOrder OrderBy {get; set;}
        public COATemplateDetailSelect Selects {get; set;}
    }

    public enum COATemplateDetailOrder
    {
        
        Code,
        Name,
        Description,
    }

    public enum COATemplateDetailSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        COATemplate = E._2,
        Code = E._3,
        Name = E._4,
        Description = E._5,
        Parent = E._6,
        BusinessGroup = E._7,
    }
}

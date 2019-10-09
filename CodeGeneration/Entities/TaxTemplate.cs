
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class TaxTemplate : DataEntity
    {
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class TaxTemplateFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Type { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public TaxTemplateOrder OrderBy {get; set;}
        public TaxTemplateSelect Selects {get; set;}
    }

    public enum TaxTemplateOrder
    {
        
        Name,
        Type,
    }

    public enum TaxTemplateSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Type = E._3,
        BusinessGroup = E._4,
    }
}

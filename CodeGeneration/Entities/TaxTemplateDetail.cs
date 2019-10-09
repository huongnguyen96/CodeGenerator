
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class TaxTemplateDetail : DataEntity
    {
        public Guid Id { get; set; }
		public Guid TaxTemplateId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid UnitOfMeasureId { get; set; }
		public decimal Rate { get; set; }
		public string Description { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class TaxTemplateDetailFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter TaxTemplateId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter UnitOfMeasureId { get; set; }
		public DecimalFilter Rate { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public TaxTemplateDetailOrder OrderBy {get; set;}
        public TaxTemplateDetailSelect Selects {get; set;}
    }

    public enum TaxTemplateDetailOrder
    {
        
        Code,
        Name,
        Rate,
        Description,
    }

    public enum TaxTemplateDetailSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        TaxTemplate = E._2,
        Code = E._3,
        Name = E._4,
        UnitOfMeasure = E._5,
        Rate = E._6,
        Description = E._7,
        BusinessGroup = E._8,
    }
}

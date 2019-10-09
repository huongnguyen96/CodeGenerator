
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class SetOfBook : DataEntity
    {
        public Guid Id { get; set; }
		public string Code { get; set; }
		public string ShortName { get; set; }
		public string Name { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid CurrencyId { get; set; }
		public bool Disabled { get; set; }
		public Guid? ChartOfAccountTemplateId { get; set; }
		public Guid? EnvironmentTaxTemplateId { get; set; }
		public Guid? ExportTaxTemplateId { get; set; }
		public Guid? ImportTaxTemplateId { get; set; }
		public Guid? NaturalResourceTaxTemplateId { get; set; }
		public Guid? SpecialConsumptionTaxTemplateId { get; set; }
		public Guid? ValueAddedTaxTemplateId { get; set; }
		
    }

    public class SetOfBookFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter ShortName { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter CurrencyId { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter ChartOfAccountTemplateId { get; set; }
		public GuidFilter EnvironmentTaxTemplateId { get; set; }
		public GuidFilter ExportTaxTemplateId { get; set; }
		public GuidFilter ImportTaxTemplateId { get; set; }
		public GuidFilter NaturalResourceTaxTemplateId { get; set; }
		public GuidFilter SpecialConsumptionTaxTemplateId { get; set; }
		public GuidFilter ValueAddedTaxTemplateId { get; set; }
		
        public SetOfBookOrder OrderBy {get; set;}
        public SetOfBookSelect Selects {get; set;}
    }

    public enum SetOfBookOrder
    {
        
        Code,
        ShortName,
        Name,
        Disabled,
    }

    public enum SetOfBookSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        ShortName = E._3,
        Name = E._4,
        BusinessGroup = E._5,
        Currency = E._6,
        Disabled = E._7,
        ChartOfAccountTemplate = E._8,
        EnvironmentTaxTemplate = E._9,
        ExportTaxTemplate = E._10,
        ImportTaxTemplate = E._11,
        NaturalResourceTaxTemplate = E._12,
        SpecialConsumptionTaxTemplate = E._13,
        ValueAddedTaxTemplate = E._14,
    }
}

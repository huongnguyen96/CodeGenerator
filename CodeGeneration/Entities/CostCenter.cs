
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class CostCenter : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid? ParentId { get; set; }
		public Guid? ChartOfAccountId { get; set; }
		public Guid SetOfBookId { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid? ValidFrom { get; set; }
		public Guid? ValidTo { get; set; }
		public string Description { get; set; }
		
    }

    public class CostCenterFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter ParentId { get; set; }
		public GuidFilter ChartOfAccountId { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter ValidFrom { get; set; }
		public GuidFilter ValidTo { get; set; }
		public StringFilter Description { get; set; }
		
        public CostCenterOrder OrderBy {get; set;}
        public CostCenterSelect Selects {get; set;}
    }

    public enum CostCenterOrder
    {
        
        Disabled,
        Code,
        Name,
        ValidFrom,
        ValidTo,
        Description,
    }

    public enum CostCenterSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        Name = E._4,
        Parent = E._5,
        ChartOfAccount = E._6,
        SetOfBook = E._7,
        BusinessGroup = E._8,
        ValidFrom = E._9,
        ValidTo = E._10,
        Description = E._11,
    }
}

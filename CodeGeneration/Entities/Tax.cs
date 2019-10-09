
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Tax : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid SetOfBookId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid? Rate { get; set; }
		public string Description { get; set; }
		public Guid? UnitOfMeasureId { get; set; }
		public string Type { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid? ParentId { get; set; }
		
    }

    public class TaxFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter Rate { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter UnitOfMeasureId { get; set; }
		public StringFilter Type { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter ParentId { get; set; }
		
        public TaxOrder OrderBy {get; set;}
        public TaxSelect Selects {get; set;}
    }

    public enum TaxOrder
    {
        
        Disabled,
        Code,
        Name,
        Rate,
        Description,
        Type,
    }

    public enum TaxSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        SetOfBook = E._3,
        Code = E._4,
        Name = E._5,
        Rate = E._6,
        Description = E._7,
        UnitOfMeasure = E._8,
        Type = E._9,
        BusinessGroup = E._10,
        Parent = E._11,
    }
}

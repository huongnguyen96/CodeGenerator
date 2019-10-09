
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class ChartOfAccount : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid SetOfBookId { get; set; }
		public Guid? ParentId { get; set; }
		public string AccountCode { get; set; }
		public string AliasCode { get; set; }
		public string AccountName { get; set; }
		public string AccountDescription { get; set; }
		public Guid? Characteristic { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class ChartOfAccountFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public GuidFilter ParentId { get; set; }
		public StringFilter AccountCode { get; set; }
		public StringFilter AliasCode { get; set; }
		public StringFilter AccountName { get; set; }
		public StringFilter AccountDescription { get; set; }
		public GuidFilter Characteristic { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public ChartOfAccountOrder OrderBy {get; set;}
        public ChartOfAccountSelect Selects {get; set;}
    }

    public enum ChartOfAccountOrder
    {
        
        Disabled,
        AccountCode,
        AliasCode,
        AccountName,
        AccountDescription,
        Characteristic,
    }

    public enum ChartOfAccountSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        SetOfBook = E._3,
        Parent = E._4,
        AccountCode = E._5,
        AliasCode = E._6,
        AccountName = E._7,
        AccountDescription = E._8,
        Characteristic = E._9,
        BusinessGroup = E._10,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class BankAccount : DataEntity
    {
        public Guid Id { get; set; }
		public Guid BankId { get; set; }
		public Guid SetOfBookId { get; set; }
		public bool Disabled { get; set; }
		public string No { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid ChartOfAccountId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class BankAccountFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter BankId { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter No { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter ChartOfAccountId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public BankAccountOrder OrderBy {get; set;}
        public BankAccountSelect Selects {get; set;}
    }

    public enum BankAccountOrder
    {
        
        Disabled,
        No,
        Name,
        Description,
    }

    public enum BankAccountSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Bank = E._2,
        SetOfBook = E._3,
        Disabled = E._4,
        No = E._5,
        Name = E._6,
        Description = E._7,
        ChartOfAccount = E._8,
        BusinessGroup = E._9,
    }
}

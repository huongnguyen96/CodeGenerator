
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class AccountingPeriod : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid FiscalYearId { get; set; }
		public DateTime StartPeriod { get; set; }
		public DateTime EndPeriod { get; set; }
		public Guid StatusId { get; set; }
		public string Description { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class AccountingPeriodFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter FiscalYearId { get; set; }
		public DateTimeFilter StartPeriod { get; set; }
		public DateTimeFilter EndPeriod { get; set; }
		public GuidFilter StatusId { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public AccountingPeriodOrder OrderBy {get; set;}
        public AccountingPeriodSelect Selects {get; set;}
    }

    public enum AccountingPeriodOrder
    {
        
        Disabled,
        StartPeriod,
        EndPeriod,
        Description,
    }

    public enum AccountingPeriodSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        FiscalYear = E._3,
        StartPeriod = E._4,
        EndPeriod = E._5,
        Status = E._6,
        Description = E._7,
        BusinessGroup = E._8,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class FiscalYear : DataEntity
    {
        public Guid Id { get; set; }
		public Guid SetOfBookId { get; set; }
		public bool Disabled { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string InventoryValuationMethod { get; set; }
		public Guid StatusId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class FiscalYearFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Name { get; set; }
		public DateTimeFilter StartDate { get; set; }
		public DateTimeFilter EndDate { get; set; }
		public StringFilter InventoryValuationMethod { get; set; }
		public GuidFilter StatusId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public FiscalYearOrder OrderBy {get; set;}
        public FiscalYearSelect Selects {get; set;}
    }

    public enum FiscalYearOrder
    {
        
        Disabled,
        Name,
        StartDate,
        EndDate,
        InventoryValuationMethod,
    }

    public enum FiscalYearSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        SetOfBook = E._2,
        Disabled = E._3,
        Name = E._4,
        StartDate = E._5,
        EndDate = E._6,
        InventoryValuationMethod = E._7,
        Status = E._8,
        BusinessGroup = E._9,
    }
}

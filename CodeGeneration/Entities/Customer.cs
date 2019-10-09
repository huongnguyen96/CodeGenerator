
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Customer : DataEntity
    {
        public Guid Id { get; set; }
		public Guid BusinessGroupId { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string TaxCode { get; set; }
		public string Description { get; set; }
		public Guid StatusId { get; set; }
		public string Note { get; set; }
		
    }

    public class CustomerFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Address { get; set; }
		public StringFilter TaxCode { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter StatusId { get; set; }
		public StringFilter Note { get; set; }
		
        public CustomerOrder OrderBy {get; set;}
        public CustomerSelect Selects {get; set;}
    }

    public enum CustomerOrder
    {
        
        Disabled,
        Code,
        Name,
        Address,
        TaxCode,
        Description,
        Note,
    }

    public enum CustomerSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        BusinessGroup = E._2,
        Disabled = E._3,
        Code = E._4,
        Name = E._5,
        Address = E._6,
        TaxCode = E._7,
        Description = E._8,
        Status = E._9,
        Note = E._10,
    }
}

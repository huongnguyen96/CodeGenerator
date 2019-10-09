
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Bank : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid BusinessGroupId { get; set; }
		public string Description { get; set; }
		
    }

    public class BankFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public StringFilter Description { get; set; }
		
        public BankOrder OrderBy {get; set;}
        public BankSelect Selects {get; set;}
    }

    public enum BankOrder
    {
        
        Disabled,
        Code,
        Name,
        Description,
    }

    public enum BankSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        Name = E._4,
        BusinessGroup = E._5,
        Description = E._6,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Currency : DataEntity
    {
        public Guid Id { get; set; }
		public Guid BusinessGroupId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public bool Disabled { get; set; }
		public int Sequence { get; set; }
		public string Description { get; set; }
		
    }

    public class CurrencyFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public bool? Disabled { get; set; }
		public IntFilter Sequence { get; set; }
		public StringFilter Description { get; set; }
		
        public CurrencyOrder OrderBy {get; set;}
        public CurrencySelect Selects {get; set;}
    }

    public enum CurrencyOrder
    {
        
        Code,
        Name,
        Disabled,
        Sequence,
        Description,
    }

    public enum CurrencySelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        BusinessGroup = E._2,
        Code = E._3,
        Name = E._4,
        Disabled = E._5,
        Sequence = E._6,
        Description = E._7,
    }
}

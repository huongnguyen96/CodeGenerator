
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class PaymentMethod : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid SetOfBookId { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class PaymentMethodFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public PaymentMethodOrder OrderBy {get; set;}
        public PaymentMethodSelect Selects {get; set;}
    }

    public enum PaymentMethodOrder
    {
        
        Disabled,
        Code,
        Name,
    }

    public enum PaymentMethodSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Code = E._3,
        Name = E._4,
        SetOfBook = E._5,
        BusinessGroup = E._6,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class PaymentTerm : DataEntity
    {
        public Guid Id { get; set; }
		public Guid SetOfBookId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public Guid? DueInDays { get; set; }
		public Guid? DiscountPeriod { get; set; }
		public bool Disabled { get; set; }
		public Guid? DiscountRate { get; set; }
		public int Sequence { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class PaymentTermFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public GuidFilter DueInDays { get; set; }
		public GuidFilter DiscountPeriod { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter DiscountRate { get; set; }
		public IntFilter Sequence { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public PaymentTermOrder OrderBy {get; set;}
        public PaymentTermSelect Selects {get; set;}
    }

    public enum PaymentTermOrder
    {
        
        Code,
        Name,
        DueInDays,
        DiscountPeriod,
        Disabled,
        DiscountRate,
        Sequence,
    }

    public enum PaymentTermSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        SetOfBook = E._2,
        Code = E._3,
        Name = E._4,
        DueInDays = E._5,
        DiscountPeriod = E._6,
        Disabled = E._7,
        DiscountRate = E._8,
        Sequence = E._9,
        BusinessGroup = E._10,
    }
}

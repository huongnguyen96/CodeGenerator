
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Voucher : DataEntity
    {
        public Guid Id { get; set; }
		public Guid SetOfBookId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid? DebitAccountId { get; set; }
		public Guid? CreditAccountId { get; set; }
		public Guid? VoucherTypeId { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class VoucherFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter SetOfBookId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter Description { get; set; }
		public GuidFilter DebitAccountId { get; set; }
		public GuidFilter CreditAccountId { get; set; }
		public GuidFilter VoucherTypeId { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public VoucherOrder OrderBy {get; set;}
        public VoucherSelect Selects {get; set;}
    }

    public enum VoucherOrder
    {
        
        Code,
        Name,
        Description,
        Disabled,
    }

    public enum VoucherSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        SetOfBook = E._2,
        Code = E._3,
        Name = E._4,
        Description = E._5,
        DebitAccount = E._6,
        CreditAccount = E._7,
        VoucherType = E._8,
        Disabled = E._9,
        BusinessGroup = E._10,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class VoucherType : DataEntity
    {
        public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class VoucherTypeFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public VoucherTypeOrder OrderBy {get; set;}
        public VoucherTypeSelect Selects {get; set;}
    }

    public enum VoucherTypeOrder
    {
        
        Code,
        Name,
        Disabled,
    }

    public enum VoucherTypeSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        Disabled = E._4,
        BusinessGroup = E._5,
    }
}

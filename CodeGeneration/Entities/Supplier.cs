
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class Supplier : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string TaxCode { get; set; }
		public Guid StatusId { get; set; }
		public string Note { get; set; }
		
    }

    public class SupplierFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public StringFilter Code { get; set; }
		public StringFilter Name { get; set; }
		public StringFilter TaxCode { get; set; }
		public GuidFilter StatusId { get; set; }
		public StringFilter Note { get; set; }
		
        public SupplierOrder OrderBy {get; set;}
        public SupplierSelect Selects {get; set;}
    }

    public enum SupplierOrder
    {
        
        Disabled,
        Code,
        Name,
        TaxCode,
        Note,
    }

    public enum SupplierSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        BusinessGroup = E._3,
        Code = E._4,
        Name = E._5,
        TaxCode = E._6,
        Status = E._7,
        Note = E._8,
    }
}

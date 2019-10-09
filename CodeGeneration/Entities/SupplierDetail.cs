
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class SupplierDetail : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid LegalEntityId { get; set; }
		public Guid SupplierId { get; set; }
		public Guid? StaffInChargeId { get; set; }
		public Guid? PaymentTermId { get; set; }
		public Guid? DebtLoad { get; set; }
		public Guid? DueInDays { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class SupplierDetailFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public GuidFilter SupplierId { get; set; }
		public GuidFilter StaffInChargeId { get; set; }
		public GuidFilter PaymentTermId { get; set; }
		public GuidFilter DebtLoad { get; set; }
		public GuidFilter DueInDays { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public SupplierDetailOrder OrderBy {get; set;}
        public SupplierDetailSelect Selects {get; set;}
    }

    public enum SupplierDetailOrder
    {
        
        Disabled,
        DebtLoad,
        DueInDays,
    }

    public enum SupplierDetailSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        LegalEntity = E._3,
        Supplier = E._4,
        StaffInCharge = E._5,
        PaymentTerm = E._6,
        DebtLoad = E._7,
        DueInDays = E._8,
        BusinessGroup = E._9,
    }
}

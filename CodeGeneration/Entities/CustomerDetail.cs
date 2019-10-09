
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class CustomerDetail : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid BusinessGroupId { get; set; }
		public Guid CustomerId { get; set; }
		public Guid LegalEntityId { get; set; }
		public Guid? PaymentTermId { get; set; }
		public Guid? DueInDays { get; set; }
		public Guid? DebtLoad { get; set; }
		public Guid? StaffInChargeId { get; set; }
		
    }

    public class CustomerDetailFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public GuidFilter CustomerId { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public GuidFilter PaymentTermId { get; set; }
		public GuidFilter DueInDays { get; set; }
		public GuidFilter DebtLoad { get; set; }
		public GuidFilter StaffInChargeId { get; set; }
		
        public CustomerDetailOrder OrderBy {get; set;}
        public CustomerDetailSelect Selects {get; set;}
    }

    public enum CustomerDetailOrder
    {
        
        Disabled,
        DueInDays,
        DebtLoad,
    }

    public enum CustomerDetailSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        BusinessGroup = E._3,
        Customer = E._4,
        LegalEntity = E._5,
        PaymentTerm = E._6,
        DueInDays = E._7,
        DebtLoad = E._8,
        StaffInCharge = E._9,
    }
}

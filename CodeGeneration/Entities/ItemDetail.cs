
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class ItemDetail : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid ItemId { get; set; }
		public Guid LegalEntityId { get; set; }
		public Guid? DefaultValue { get; set; }
		public Guid? InventoryAccountId { get; set; }
		public Guid? ReturnAccountId { get; set; }
		public Guid? SalesAllowancesAccountId { get; set; }
		public Guid? ExpenseAccountId { get; set; }
		public Guid? RevenueAccountId { get; set; }
		public Guid? DiscountAccountId { get; set; }
		public bool IsDiscounted { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class ItemDetailFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter ItemId { get; set; }
		public GuidFilter LegalEntityId { get; set; }
		public GuidFilter DefaultValue { get; set; }
		public GuidFilter InventoryAccountId { get; set; }
		public GuidFilter ReturnAccountId { get; set; }
		public GuidFilter SalesAllowancesAccountId { get; set; }
		public GuidFilter ExpenseAccountId { get; set; }
		public GuidFilter RevenueAccountId { get; set; }
		public GuidFilter DiscountAccountId { get; set; }
		public bool? IsDiscounted { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public ItemDetailOrder OrderBy {get; set;}
        public ItemDetailSelect Selects {get; set;}
    }

    public enum ItemDetailOrder
    {
        
        Disabled,
        DefaultValue,
        IsDiscounted,
    }

    public enum ItemDetailSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        Item = E._3,
        LegalEntity = E._4,
        DefaultValue = E._5,
        InventoryAccount = E._6,
        ReturnAccount = E._7,
        SalesAllowancesAccount = E._8,
        ExpenseAccount = E._9,
        RevenueAccount = E._10,
        DiscountAccount = E._11,
        IsDiscounted = E._12,
        BusinessGroup = E._13,
    }
}

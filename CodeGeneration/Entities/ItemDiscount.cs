
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class ItemDiscount : DataEntity
    {
        public Guid Id { get; set; }
		public bool Disabled { get; set; }
		public Guid ItemDetailId { get; set; }
		public int QuantityFrom { get; set; }
		public int QuantityTo { get; set; }
		public Guid? Rate { get; set; }
		public string DiscountType { get; set; }
		public Guid BusinessGroupId { get; set; }
		
    }

    public class ItemDiscountFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter ItemDetailId { get; set; }
		public IntFilter QuantityFrom { get; set; }
		public IntFilter QuantityTo { get; set; }
		public GuidFilter Rate { get; set; }
		public StringFilter DiscountType { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		
        public ItemDiscountOrder OrderBy {get; set;}
        public ItemDiscountSelect Selects {get; set;}
    }

    public enum ItemDiscountOrder
    {
        
        Disabled,
        QuantityFrom,
        QuantityTo,
        Rate,
        DiscountType,
    }

    public enum ItemDiscountSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Disabled = E._2,
        ItemDetail = E._3,
        QuantityFrom = E._4,
        QuantityTo = E._5,
        Rate = E._6,
        DiscountType = E._7,
        BusinessGroup = E._8,
    }
}

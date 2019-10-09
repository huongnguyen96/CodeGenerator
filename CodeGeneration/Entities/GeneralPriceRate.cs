
using System;
using System.Collections.Generic;
using Common;

namespace ERP.Entities
{
    public class GeneralPriceRate : DataEntity
    {
        public Guid Id { get; set; }
		public Guid BusinessGroupId { get; set; }
		public bool Disabled { get; set; }
		public Guid ItemId { get; set; }
		public Guid UnitOfMeasureId { get; set; }
		public Guid? Price { get; set; }
		
    }

    public class GeneralPriceRateFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
		public GuidFilter BusinessGroupId { get; set; }
		public bool? Disabled { get; set; }
		public GuidFilter ItemId { get; set; }
		public GuidFilter UnitOfMeasureId { get; set; }
		public GuidFilter Price { get; set; }
		
        public GeneralPriceRateOrder OrderBy {get; set;}
        public GeneralPriceRateSelect Selects {get; set;}
    }

    public enum GeneralPriceRateOrder
    {
        
        Disabled,
        Price,
    }

    public enum GeneralPriceRateSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        BusinessGroup = E._2,
        Disabled = E._3,
        Item = E._4,
        UnitOfMeasure = E._5,
        Price = E._6,
    }
}

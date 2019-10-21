
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Unit : DataEntity
    {
        
        public long Id { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public long? ThirdVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public Variation FirstVariation { get; set; }
        public Variation SecondVariation { get; set; }
        public Variation ThirdVariation { get; set; }
        public List<DiscountItem> DiscountItems { get; set; }
        public List<Stock> Stocks { get; set; }
    }

    public class UnitFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter FirstVariationId { get; set; }
        public LongFilter SecondVariationId { get; set; }
        public LongFilter ThirdVariationId { get; set; }
        public StringFilter SKU { get; set; }
        public LongFilter Price { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public UnitOrder OrderBy {get; set;}
        public UnitSelect Selects {get; set;}
    }

    public enum UnitOrder
    {
        
        Id = 1,
        FirstVariation = 2,
        SecondVariation = 3,
        ThirdVariation = 4,
        SKU = 5,
        Price = 6,
    }

    public enum UnitSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        FirstVariation = E._2,
        SecondVariation = E._3,
        ThirdVariation = E._4,
        SKU = E._5,
        Price = E._6,
    }
}


using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Variation : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }
        public VariationGrouping VariationGrouping { get; set; }
        public List<Unit> UnitFirstVariations { get; set; }
        public List<Unit> UnitSecondVariations { get; set; }
        public List<Unit> UnitThirdVariations { get; set; }
    }

    public class VariationFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter VariationGroupingId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public VariationOrder OrderBy {get; set;}
        public VariationSelect Selects {get; set;}
    }

    public enum VariationOrder
    {
        
        Id = 1,
        Name = 2,
        VariationGrouping = 3,
    }

    public enum VariationSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        VariationGrouping = E._3,
    }
}

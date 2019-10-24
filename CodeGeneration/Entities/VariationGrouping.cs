
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class VariationGrouping : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public List<Variation> Variations { get; set; }
    }

    public class VariationGroupingFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter ProductId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public VariationGroupingOrder OrderBy {get; set;}
        public VariationGroupingSelect Selects {get; set;}
    }

    public enum VariationGroupingOrder
    {
        
        Id = 1,
        Name = 2,
        Product = 3,
    }
    
    [Flags]
    public enum VariationGroupingSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Product = E._3,
    }
}

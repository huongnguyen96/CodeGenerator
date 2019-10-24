
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Brand : DataEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }

    public class BrandFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter CategoryId { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public BrandOrder OrderBy {get; set;}
        public BrandSelect Selects {get; set;}
    }

    public enum BrandOrder
    {
        
        Id = 1,
        Name = 2,
        Category = 3,
    }
    
    [Flags]
    public enum BrandSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Name = E._2,
        Category = E._3,
    }
}

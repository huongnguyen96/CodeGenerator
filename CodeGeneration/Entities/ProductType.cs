
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class ProductType : DataEntity
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }

    public class ProductTypeFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ProductTypeOrder OrderBy {get; set;}
        public ProductTypeSelect Selects {get; set;}
    }

    public enum ProductTypeOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
    }
    
    [Flags]
    public enum ProductTypeSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
    }
}

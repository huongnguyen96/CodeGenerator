
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class ItemUnitOfMeasure : DataEntity
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<ItemStock> ItemStocks { get; set; }
        public List<Item> Items { get; set; }
    }

    public class ItemUnitOfMeasureFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ItemUnitOfMeasureOrder OrderBy {get; set;}
        public ItemUnitOfMeasureSelect Selects {get; set;}
    }

    public enum ItemUnitOfMeasureOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
    }

    public enum ItemUnitOfMeasureSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
    }
}

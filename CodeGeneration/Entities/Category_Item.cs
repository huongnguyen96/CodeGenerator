
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Category_Item : DataEntity
    {
        
        public long CategoryId { get; set; }
        public long ItemId { get; set; }
        public Category Category { get; set; }
        public Item Item { get; set; }
    }

    public class Category_ItemFilter : FilterEntity
    {
        
        public LongFilter CategoryId { get; set; }
        public LongFilter ItemId { get; set; }
        public Category_ItemOrder OrderBy {get; set;}
        public Category_ItemSelect Selects {get; set;}
    }

    public enum Category_ItemOrder
    {
        
        Category = 1,
        Item = 2,
    }

    public enum Category_ItemSelect:long
    {
        ALL = E.ALL,
        
        Category = E._1,
        Item = E._2,
    }
}

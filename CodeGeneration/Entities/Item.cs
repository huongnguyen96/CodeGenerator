
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Item : DataEntity
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Category_Item> Category_Items { get; set; }
    }

    public class ItemFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ItemOrder OrderBy {get; set;}
        public ItemSelect Selects {get; set;}
    }

    public enum ItemOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
    }

    public enum ItemSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
    }
}

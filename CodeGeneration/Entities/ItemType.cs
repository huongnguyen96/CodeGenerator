
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class ItemType : DataEntity
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }

    public class ItemTypeFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ItemTypeOrder OrderBy {get; set;}
        public ItemTypeSelect Selects {get; set;}
    }

    public enum ItemTypeOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
    }

    public enum ItemTypeSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
    }
}

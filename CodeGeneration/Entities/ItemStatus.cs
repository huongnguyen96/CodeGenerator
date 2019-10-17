
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class ItemStatus : DataEntity
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }

    public class ItemStatusFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ItemStatusOrder OrderBy {get; set;}
        public ItemStatusSelect Selects {get; set;}
    }

    public enum ItemStatusOrder
    {
        
        Id = 1,
        Code = 2,
        Name = 3,
    }

    public enum ItemStatusSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
    }
}

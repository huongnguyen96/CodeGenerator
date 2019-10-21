
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
        public string SKU { get; set; }
        public string Description { get; set; }
        public long TypeId { get; set; }
        public long StatusId { get; set; }
        public long PartnerId { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public Partner Partner { get; set; }
        public ItemStatus Status { get; set; }
        public ItemType Type { get; set; }
        public List<VariationGrouping> VariationGroupings { get; set; }
    }

    public class ItemFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter SKU { get; set; }
        public StringFilter Description { get; set; }
        public LongFilter TypeId { get; set; }
        public LongFilter StatusId { get; set; }
        public LongFilter PartnerId { get; set; }
        public LongFilter CategoryId { get; set; }
        public LongFilter BrandId { get; set; }
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
        SKU = 4,
        Description = 5,
        Type = 6,
        Status = 7,
        Partner = 8,
        Category = 9,
        Brand = 10,
    }

    public enum ItemSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        SKU = E._4,
        Description = E._5,
        Type = E._6,
        Status = E._7,
        Partner = E._8,
        Category = E._9,
        Brand = E._10,
    }
}

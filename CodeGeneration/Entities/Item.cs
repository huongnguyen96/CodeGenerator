
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
        public long TypeId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string Description { get; set; }
        public long? StatusId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long SupplierId { get; set; }
        public ItemStatus Status { get; set; }
        public Supplier Supplier { get; set; }
        public ItemType Type { get; set; }
        public ItemUnitOfMeasure UnitOfMeasure { get; set; }
        public List<Category_Item> Category_Items { get; set; }
        public List<ItemStock> ItemStocks { get; set; }
    }

    public class ItemFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter SKU { get; set; }
        public LongFilter TypeId { get; set; }
        public DecimalFilter PurchasePrice { get; set; }
        public DecimalFilter SalePrice { get; set; }
        public StringFilter Description { get; set; }
        public LongFilter StatusId { get; set; }
        public LongFilter UnitOfMeasureId { get; set; }
        public LongFilter SupplierId { get; set; }
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
        Type = 5,
        PurchasePrice = 6,
        SalePrice = 7,
        Description = 8,
        Status = 9,
        UnitOfMeasure = 10,
        Supplier = 11,
    }

    public enum ItemSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Code = E._2,
        Name = E._3,
        SKU = E._4,
        Type = E._5,
        PurchasePrice = E._6,
        SalePrice = E._7,
        Description = E._8,
        Status = E._9,
        UnitOfMeasure = E._10,
        Supplier = E._11,
    }
}

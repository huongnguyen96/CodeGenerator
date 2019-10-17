
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class ItemStock : DataEntity
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public Item Item { get; set; }
        public ItemUnitOfMeasure UnitOfMeasure { get; set; }
        public Warehouse Warehouse { get; set; }
    }

    public class ItemStockFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter ItemId { get; set; }
        public LongFilter WarehouseId { get; set; }
        public LongFilter UnitOfMeasureId { get; set; }
        public DecimalFilter Quantity { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ItemStockOrder OrderBy {get; set;}
        public ItemStockSelect Selects {get; set;}
    }

    public enum ItemStockOrder
    {
        
        Id = 1,
        Item = 2,
        Warehouse = 3,
        UnitOfMeasure = 4,
        Quantity = 5,
    }

    public enum ItemStockSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Item = E._2,
        Warehouse = E._3,
        UnitOfMeasure = E._4,
        Quantity = E._5,
    }
}

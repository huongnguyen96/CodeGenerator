
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Stock : DataEntity
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public Item Item { get; set; }
        public Warehouse Warehouse { get; set; }
    }

    public class StockFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter ItemId { get; set; }
        public LongFilter WarehouseId { get; set; }
        public LongFilter Quantity { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public StockOrder OrderBy {get; set;}
        public StockSelect Selects {get; set;}
    }

    public enum StockOrder
    {
        
        Id = 1,
        Item = 2,
        Warehouse = 3,
        Quantity = 4,
    }
    
    [Flags]
    public enum StockSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Item = E._2,
        Warehouse = E._3,
        Quantity = E._4,
    }
}

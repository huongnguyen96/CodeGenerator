
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Stock : DataEntity
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public Unit Unit { get; set; }
        public Warehouse Warehouse { get; set; }
    }

    public class StockFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter UnitId { get; set; }
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
        Unit = 2,
        Warehouse = 3,
        Quantity = 4,
    }

    public enum StockSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Unit = E._2,
        Warehouse = E._3,
        Quantity = E._4,
    }
}

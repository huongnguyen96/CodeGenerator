
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMaster_StockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public WarehouseMaster_UnitDTO Unit { get; set; }
        public WarehouseMaster_StockDTO() {}
        public WarehouseMaster_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.UnitId = Stock.UnitId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Unit = new WarehouseMaster_UnitDTO(Stock.Unit);

        }
    }

    public class WarehouseMaster_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
    }
}

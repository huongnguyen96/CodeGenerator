
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.unit.unit_master
{
    public class UnitMaster_StockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public UnitMaster_WarehouseDTO Warehouse { get; set; }
        public UnitMaster_StockDTO() {}
        public UnitMaster_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.UnitId = Stock.UnitId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Warehouse = new UnitMaster_WarehouseDTO(Stock.Warehouse);

        }
    }

    public class UnitMaster_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
    }
}

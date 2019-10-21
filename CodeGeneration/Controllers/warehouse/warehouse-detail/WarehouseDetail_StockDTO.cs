
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_detail
{
    public class WarehouseDetail_StockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public WarehouseDetail_UnitDTO Unit { get; set; }
        public WarehouseDetail_StockDTO() {}
        public WarehouseDetail_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.UnitId = Stock.UnitId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Unit = new WarehouseDetail_UnitDTO(Stock.Unit);

        }
    }

    public class WarehouseDetail_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
    }
}

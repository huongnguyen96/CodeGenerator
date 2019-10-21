
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.unit.unit_detail
{
    public class UnitDetail_StockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public UnitDetail_WarehouseDTO Warehouse { get; set; }
        public UnitDetail_StockDTO() {}
        public UnitDetail_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.UnitId = Stock.UnitId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Warehouse = new UnitDetail_WarehouseDTO(Stock.Warehouse);

        }
    }

    public class UnitDetail_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
    }
}

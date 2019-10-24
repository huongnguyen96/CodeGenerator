
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
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public WarehouseDetail_ItemDTO Item { get; set; }
        public WarehouseDetail_StockDTO() {}
        public WarehouseDetail_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.ItemId = Stock.ItemId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Item = new WarehouseDetail_ItemDTO(Stock.Item);

        }
    }

    public class WarehouseDetail_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
        public StockOrder OrderBy { get; set; }
    }
}

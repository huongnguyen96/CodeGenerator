
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.stock.stock_master
{
    public class StockMaster_StockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public StockMaster_ItemDTO Item { get; set; }
        public StockMaster_WarehouseDTO Warehouse { get; set; }
        public StockMaster_StockDTO() {}
        public StockMaster_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.ItemId = Stock.ItemId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Item = new StockMaster_ItemDTO(Stock.Item);

            this.Warehouse = new StockMaster_WarehouseDTO(Stock.Warehouse);

        }
    }

    public class StockMaster_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
        public StockOrder OrderBy { get; set; }
    }
}

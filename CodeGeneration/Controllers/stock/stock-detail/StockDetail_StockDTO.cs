
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.stock.stock_detail
{
    public class StockDetail_StockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public StockDetail_ItemDTO Item { get; set; }
        public StockDetail_WarehouseDTO Warehouse { get; set; }
        public StockDetail_StockDTO() {}
        public StockDetail_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.ItemId = Stock.ItemId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Item = new StockDetail_ItemDTO(Stock.Item);

            this.Warehouse = new StockDetail_WarehouseDTO(Stock.Warehouse);

        }
    }

    public class StockDetail_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
        public StockOrder OrderBy { get; set; }
    }
}

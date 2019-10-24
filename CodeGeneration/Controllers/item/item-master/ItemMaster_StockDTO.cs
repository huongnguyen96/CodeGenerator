
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_master
{
    public class ItemMaster_StockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }
        public ItemMaster_WarehouseDTO Warehouse { get; set; }
        public ItemMaster_StockDTO() {}
        public ItemMaster_StockDTO(Stock Stock)
        {
            
            this.Id = Stock.Id;
            this.ItemId = Stock.ItemId;
            this.WarehouseId = Stock.WarehouseId;
            this.Quantity = Stock.Quantity;
            this.Warehouse = new ItemMaster_WarehouseDTO(Stock.Warehouse);

        }
    }

    public class ItemMaster_StockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? Quantity { get; set; }
        public StockOrder OrderBy { get; set; }
    }
}

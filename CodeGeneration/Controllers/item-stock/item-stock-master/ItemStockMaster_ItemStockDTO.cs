
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_stock.item_stock_master
{
    public class ItemStockMaster_ItemStockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public ItemStockMaster_ItemDTO Item { get; set; }
        public ItemStockMaster_ItemUnitOfMeasureDTO UnitOfMeasure { get; set; }
        public ItemStockMaster_WarehouseDTO Warehouse { get; set; }
        public ItemStockMaster_ItemStockDTO() {}
        public ItemStockMaster_ItemStockDTO(ItemStock ItemStock)
        {
            
            this.Id = ItemStock.Id;
            this.ItemId = ItemStock.ItemId;
            this.WarehouseId = ItemStock.WarehouseId;
            this.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            this.Quantity = ItemStock.Quantity;
            this.Item = new ItemStockMaster_ItemDTO(ItemStock.Item);

            this.UnitOfMeasure = new ItemStockMaster_ItemUnitOfMeasureDTO(ItemStock.UnitOfMeasure);

            this.Warehouse = new ItemStockMaster_WarehouseDTO(ItemStock.Warehouse);

        }
    }

    public class ItemStockMaster_ItemStockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
    }
}

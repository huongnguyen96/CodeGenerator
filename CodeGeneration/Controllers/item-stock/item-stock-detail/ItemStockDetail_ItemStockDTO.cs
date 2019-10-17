
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_stock.item_stock_detail
{
    public class ItemStockDetail_ItemStockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public ItemStockDetail_ItemDTO Item { get; set; }
        public ItemStockDetail_ItemUnitOfMeasureDTO UnitOfMeasure { get; set; }
        public ItemStockDetail_WarehouseDTO Warehouse { get; set; }
        public ItemStockDetail_ItemStockDTO() {}
        public ItemStockDetail_ItemStockDTO(ItemStock ItemStock)
        {
            
            this.Id = ItemStock.Id;
            this.ItemId = ItemStock.ItemId;
            this.WarehouseId = ItemStock.WarehouseId;
            this.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            this.Quantity = ItemStock.Quantity;
            this.Item = new ItemStockDetail_ItemDTO(ItemStock.Item);

            this.UnitOfMeasure = new ItemStockDetail_ItemUnitOfMeasureDTO(ItemStock.UnitOfMeasure);

            this.Warehouse = new ItemStockDetail_WarehouseDTO(ItemStock.Warehouse);

        }
    }

    public class ItemStockDetail_ItemStockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
    }
}

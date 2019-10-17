
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_detail
{
    public class ItemDetail_ItemStockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public ItemDetail_ItemUnitOfMeasureDTO UnitOfMeasure { get; set; }
        public ItemDetail_WarehouseDTO Warehouse { get; set; }
        public ItemDetail_ItemStockDTO() {}
        public ItemDetail_ItemStockDTO(ItemStock ItemStock)
        {
            
            this.Id = ItemStock.Id;
            this.ItemId = ItemStock.ItemId;
            this.WarehouseId = ItemStock.WarehouseId;
            this.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            this.Quantity = ItemStock.Quantity;
            this.UnitOfMeasure = new ItemDetail_ItemUnitOfMeasureDTO(ItemStock.UnitOfMeasure);

            this.Warehouse = new ItemDetail_WarehouseDTO(ItemStock.Warehouse);

        }
    }

    public class ItemDetail_ItemStockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
    }
}

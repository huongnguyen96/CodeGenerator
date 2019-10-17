
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_detail
{
    public class WarehouseDetail_ItemStockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public WarehouseDetail_ItemDTO Item { get; set; }
        public WarehouseDetail_ItemUnitOfMeasureDTO UnitOfMeasure { get; set; }
        public WarehouseDetail_ItemStockDTO() {}
        public WarehouseDetail_ItemStockDTO(ItemStock ItemStock)
        {
            
            this.Id = ItemStock.Id;
            this.ItemId = ItemStock.ItemId;
            this.WarehouseId = ItemStock.WarehouseId;
            this.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            this.Quantity = ItemStock.Quantity;
            this.Item = new WarehouseDetail_ItemDTO(ItemStock.Item);

            this.UnitOfMeasure = new WarehouseDetail_ItemUnitOfMeasureDTO(ItemStock.UnitOfMeasure);

        }
    }

    public class WarehouseDetail_ItemStockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
    }
}

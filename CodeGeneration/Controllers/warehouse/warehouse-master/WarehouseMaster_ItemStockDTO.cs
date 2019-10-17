
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMaster_ItemStockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public WarehouseMaster_ItemDTO Item { get; set; }
        public WarehouseMaster_ItemUnitOfMeasureDTO UnitOfMeasure { get; set; }
        public WarehouseMaster_ItemStockDTO() {}
        public WarehouseMaster_ItemStockDTO(ItemStock ItemStock)
        {
            
            this.Id = ItemStock.Id;
            this.ItemId = ItemStock.ItemId;
            this.WarehouseId = ItemStock.WarehouseId;
            this.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            this.Quantity = ItemStock.Quantity;
            this.Item = new WarehouseMaster_ItemDTO(ItemStock.Item);

            this.UnitOfMeasure = new WarehouseMaster_ItemUnitOfMeasureDTO(ItemStock.UnitOfMeasure);

        }
    }

    public class WarehouseMaster_ItemStockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
    }
}

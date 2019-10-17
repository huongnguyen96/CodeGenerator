
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_unit_of_measure.item_unit_of_measure_master
{
    public class ItemUnitOfMeasureMaster_ItemStockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public ItemUnitOfMeasureMaster_ItemDTO Item { get; set; }
        public ItemUnitOfMeasureMaster_WarehouseDTO Warehouse { get; set; }
        public ItemUnitOfMeasureMaster_ItemStockDTO() {}
        public ItemUnitOfMeasureMaster_ItemStockDTO(ItemStock ItemStock)
        {
            
            this.Id = ItemStock.Id;
            this.ItemId = ItemStock.ItemId;
            this.WarehouseId = ItemStock.WarehouseId;
            this.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            this.Quantity = ItemStock.Quantity;
            this.Item = new ItemUnitOfMeasureMaster_ItemDTO(ItemStock.Item);

            this.Warehouse = new ItemUnitOfMeasureMaster_WarehouseDTO(ItemStock.Warehouse);

        }
    }

    public class ItemUnitOfMeasureMaster_ItemStockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
    }
}

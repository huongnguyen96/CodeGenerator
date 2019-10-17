
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_unit_of_measure.item_unit_of_measure_detail
{
    public class ItemUnitOfMeasureDetail_ItemStockDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public ItemUnitOfMeasureDetail_ItemDTO Item { get; set; }
        public ItemUnitOfMeasureDetail_WarehouseDTO Warehouse { get; set; }
        public ItemUnitOfMeasureDetail_ItemStockDTO() {}
        public ItemUnitOfMeasureDetail_ItemStockDTO(ItemStock ItemStock)
        {
            
            this.Id = ItemStock.Id;
            this.ItemId = ItemStock.ItemId;
            this.WarehouseId = ItemStock.WarehouseId;
            this.UnitOfMeasureId = ItemStock.UnitOfMeasureId;
            this.Quantity = ItemStock.Quantity;
            this.Item = new ItemUnitOfMeasureDetail_ItemDTO(ItemStock.Item);

            this.Warehouse = new ItemUnitOfMeasureDetail_WarehouseDTO(ItemStock.Warehouse);

        }
    }

    public class ItemUnitOfMeasureDetail_ItemStockFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? WarehouseId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_status.item_status_master
{
    public class ItemStatusMaster_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public long TypeId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string Description { get; set; }
        public long? StatusId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long SupplierId { get; set; }
        public ItemStatusMaster_SupplierDTO Supplier { get; set; }
        public ItemStatusMaster_ItemTypeDTO Type { get; set; }
        public ItemStatusMaster_ItemUnitOfMeasureDTO UnitOfMeasure { get; set; }
        public ItemStatusMaster_ItemDTO() {}
        public ItemStatusMaster_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.Code = Item.Code;
            this.Name = Item.Name;
            this.SKU = Item.SKU;
            this.TypeId = Item.TypeId;
            this.PurchasePrice = Item.PurchasePrice;
            this.SalePrice = Item.SalePrice;
            this.Description = Item.Description;
            this.StatusId = Item.StatusId;
            this.UnitOfMeasureId = Item.UnitOfMeasureId;
            this.SupplierId = Item.SupplierId;
            this.Supplier = new ItemStatusMaster_SupplierDTO(Item.Supplier);

            this.Type = new ItemStatusMaster_ItemTypeDTO(Item.Type);

            this.UnitOfMeasure = new ItemStatusMaster_ItemUnitOfMeasureDTO(Item.UnitOfMeasure);

        }
    }

    public class ItemStatusMaster_ItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public long? TypeId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string Description { get; set; }
        public long? StatusId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public long? SupplierId { get; set; }
    }
}

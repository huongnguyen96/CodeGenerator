
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_type.item_type_master
{
    public class ItemTypeMaster_ItemDTO : DataDTO
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
        public ItemTypeMaster_ItemStatusDTO Status { get; set; }
        public ItemTypeMaster_SupplierDTO Supplier { get; set; }
        public ItemTypeMaster_ItemUnitOfMeasureDTO UnitOfMeasure { get; set; }
        public ItemTypeMaster_ItemDTO() {}
        public ItemTypeMaster_ItemDTO(Item Item)
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
            this.Status = new ItemTypeMaster_ItemStatusDTO(Item.Status);

            this.Supplier = new ItemTypeMaster_SupplierDTO(Item.Supplier);

            this.UnitOfMeasure = new ItemTypeMaster_ItemUnitOfMeasureDTO(Item.UnitOfMeasure);

        }
    }

    public class ItemTypeMaster_ItemFilterDTO : FilterDTO
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

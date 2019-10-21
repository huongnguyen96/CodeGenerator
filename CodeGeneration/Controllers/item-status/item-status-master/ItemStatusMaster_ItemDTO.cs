
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
        public string Description { get; set; }
        public long TypeId { get; set; }
        public long StatusId { get; set; }
        public long PartnerId { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public ItemStatusMaster_BrandDTO Brand { get; set; }
        public ItemStatusMaster_CategoryDTO Category { get; set; }
        public ItemStatusMaster_PartnerDTO Partner { get; set; }
        public ItemStatusMaster_ItemTypeDTO Type { get; set; }
        public ItemStatusMaster_ItemDTO() {}
        public ItemStatusMaster_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.Code = Item.Code;
            this.Name = Item.Name;
            this.SKU = Item.SKU;
            this.Description = Item.Description;
            this.TypeId = Item.TypeId;
            this.StatusId = Item.StatusId;
            this.PartnerId = Item.PartnerId;
            this.CategoryId = Item.CategoryId;
            this.BrandId = Item.BrandId;
            this.Brand = new ItemStatusMaster_BrandDTO(Item.Brand);

            this.Category = new ItemStatusMaster_CategoryDTO(Item.Category);

            this.Partner = new ItemStatusMaster_PartnerDTO(Item.Partner);

            this.Type = new ItemStatusMaster_ItemTypeDTO(Item.Type);

        }
    }

    public class ItemStatusMaster_ItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public long? TypeId { get; set; }
        public long? StatusId { get; set; }
        public long? PartnerId { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
    }
}

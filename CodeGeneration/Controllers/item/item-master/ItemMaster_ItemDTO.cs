
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_master
{
    public class ItemMaster_ItemDTO : DataDTO
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
        public ItemMaster_ItemDTO() {}
        public ItemMaster_ItemDTO(Item Item)
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
        }
    }

    public class ItemMaster_ItemFilterDTO : FilterDTO
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

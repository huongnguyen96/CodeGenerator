
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.brand.brand_master
{
    public class BrandMaster_ItemDTO : DataDTO
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
        public BrandMaster_CategoryDTO Category { get; set; }
        public BrandMaster_PartnerDTO Partner { get; set; }
        public BrandMaster_ItemStatusDTO Status { get; set; }
        public BrandMaster_ItemTypeDTO Type { get; set; }
        public BrandMaster_ItemDTO() {}
        public BrandMaster_ItemDTO(Item Item)
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
            this.Category = new BrandMaster_CategoryDTO(Item.Category);

            this.Partner = new BrandMaster_PartnerDTO(Item.Partner);

            this.Status = new BrandMaster_ItemStatusDTO(Item.Status);

            this.Type = new BrandMaster_ItemTypeDTO(Item.Type);

        }
    }

    public class BrandMaster_ItemFilterDTO : FilterDTO
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

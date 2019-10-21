
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.category.category_master
{
    public class CategoryMaster_ItemDTO : DataDTO
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
        public CategoryMaster_BrandDTO Brand { get; set; }
        public CategoryMaster_PartnerDTO Partner { get; set; }
        public CategoryMaster_ItemStatusDTO Status { get; set; }
        public CategoryMaster_ItemTypeDTO Type { get; set; }
        public CategoryMaster_ItemDTO() {}
        public CategoryMaster_ItemDTO(Item Item)
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
            this.Brand = new CategoryMaster_BrandDTO(Item.Brand);

            this.Partner = new CategoryMaster_PartnerDTO(Item.Partner);

            this.Status = new CategoryMaster_ItemStatusDTO(Item.Status);

            this.Type = new CategoryMaster_ItemTypeDTO(Item.Type);

        }
    }

    public class CategoryMaster_ItemFilterDTO : FilterDTO
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

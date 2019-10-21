
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.partner.partner_detail
{
    public class PartnerDetail_ItemDTO : DataDTO
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
        public PartnerDetail_BrandDTO Brand { get; set; }
        public PartnerDetail_CategoryDTO Category { get; set; }
        public PartnerDetail_ItemStatusDTO Status { get; set; }
        public PartnerDetail_ItemTypeDTO Type { get; set; }
        public PartnerDetail_ItemDTO() {}
        public PartnerDetail_ItemDTO(Item Item)
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
            this.Brand = new PartnerDetail_BrandDTO(Item.Brand);

            this.Category = new PartnerDetail_CategoryDTO(Item.Category);

            this.Status = new PartnerDetail_ItemStatusDTO(Item.Status);

            this.Type = new PartnerDetail_ItemTypeDTO(Item.Type);

        }
    }

    public class PartnerDetail_ItemFilterDTO : FilterDTO
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

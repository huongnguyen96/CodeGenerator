
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.brand.brand_detail
{
    public class BrandDetail_ItemTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public BrandDetail_ItemTypeDTO() {}
        public BrandDetail_ItemTypeDTO(ItemType ItemType)
        {
            
            this.Id = ItemType.Id;
            this.Code = ItemType.Code;
            this.Name = ItemType.Name;
        }
    }

    public class BrandDetail_ItemTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.brand.brand_master
{
    public class BrandMaster_ItemTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public BrandMaster_ItemTypeDTO() {}
        public BrandMaster_ItemTypeDTO(ItemType ItemType)
        {
            
            this.Id = ItemType.Id;
            this.Code = ItemType.Code;
            this.Name = ItemType.Name;
        }
    }

    public class BrandMaster_ItemTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

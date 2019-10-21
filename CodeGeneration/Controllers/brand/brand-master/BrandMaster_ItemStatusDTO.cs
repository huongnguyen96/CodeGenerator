
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.brand.brand_master
{
    public class BrandMaster_ItemStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public BrandMaster_ItemStatusDTO() {}
        public BrandMaster_ItemStatusDTO(ItemStatus ItemStatus)
        {
            
            this.Id = ItemStatus.Id;
            this.Code = ItemStatus.Code;
            this.Name = ItemStatus.Name;
        }
    }

    public class BrandMaster_ItemStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

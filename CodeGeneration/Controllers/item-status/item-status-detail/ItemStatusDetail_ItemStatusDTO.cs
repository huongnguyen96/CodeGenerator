
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_status.item_status_detail
{
    public class ItemStatusDetail_ItemStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemStatusDetail_ItemStatusDTO() {}
        public ItemStatusDetail_ItemStatusDTO(ItemStatus ItemStatus)
        {
            
            this.Id = ItemStatus.Id;
            this.Code = ItemStatus.Code;
            this.Name = ItemStatus.Name;
        }
    }

    public class ItemStatusDetail_ItemStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

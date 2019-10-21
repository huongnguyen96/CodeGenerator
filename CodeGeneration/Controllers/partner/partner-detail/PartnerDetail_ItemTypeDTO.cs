
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.partner.partner_detail
{
    public class PartnerDetail_ItemTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public PartnerDetail_ItemTypeDTO() {}
        public PartnerDetail_ItemTypeDTO(ItemType ItemType)
        {
            
            this.Id = ItemType.Id;
            this.Code = ItemType.Code;
            this.Name = ItemType.Name;
        }
    }

    public class PartnerDetail_ItemTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.partner.partner_detail
{
    public class PartnerDetail_ItemStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public PartnerDetail_ItemStatusDTO() {}
        public PartnerDetail_ItemStatusDTO(ItemStatus ItemStatus)
        {
            
            this.Id = ItemStatus.Id;
            this.Code = ItemStatus.Code;
            this.Name = ItemStatus.Name;
        }
    }

    public class PartnerDetail_ItemStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

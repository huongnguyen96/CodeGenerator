
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_master
{
    public class ItemMaster_PartnerDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public ItemMaster_PartnerDTO() {}
        public ItemMaster_PartnerDTO(Partner Partner)
        {
            
            this.Id = Partner.Id;
            this.Name = Partner.Name;
            this.Phone = Partner.Phone;
            this.ContactPerson = Partner.ContactPerson;
            this.Address = Partner.Address;
        }
    }

    public class ItemMaster_PartnerFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
    }
}

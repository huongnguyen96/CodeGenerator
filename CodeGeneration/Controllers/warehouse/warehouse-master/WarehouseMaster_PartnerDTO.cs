
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMaster_PartnerDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public WarehouseMaster_PartnerDTO() {}
        public WarehouseMaster_PartnerDTO(Partner Partner)
        {
            
            this.Id = Partner.Id;
            this.Name = Partner.Name;
            this.Phone = Partner.Phone;
            this.ContactPerson = Partner.ContactPerson;
            this.Address = Partner.Address;
        }
    }

    public class WarehouseMaster_PartnerFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.brand.brand_detail
{
    public class BrandDetail_PartnerDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public BrandDetail_PartnerDTO() {}
        public BrandDetail_PartnerDTO(Partner Partner)
        {
            
            this.Id = Partner.Id;
            this.Name = Partner.Name;
            this.Phone = Partner.Phone;
            this.ContactPerson = Partner.ContactPerson;
            this.Address = Partner.Address;
        }
    }

    public class BrandDetail_PartnerFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
    }
}
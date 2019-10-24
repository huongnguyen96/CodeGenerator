
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.merchant_address.merchant_address_master
{
    public class MerchantAddressMaster_MerchantDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public MerchantAddressMaster_MerchantDTO() {}
        public MerchantAddressMaster_MerchantDTO(Merchant Merchant)
        {
            
            this.Id = Merchant.Id;
            this.Name = Merchant.Name;
            this.Phone = Merchant.Phone;
            this.ContactPerson = Merchant.ContactPerson;
            this.Address = Merchant.Address;
        }
    }

    public class MerchantAddressMaster_MerchantFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public MerchantOrder OrderBy { get; set; }
    }
}

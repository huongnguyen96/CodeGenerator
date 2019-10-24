
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.merchant.merchant_master
{
    public class MerchantMaster_MerchantAddressDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long MerchantId { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public MerchantMaster_MerchantAddressDTO() {}
        public MerchantMaster_MerchantAddressDTO(MerchantAddress MerchantAddress)
        {
            
            this.Id = MerchantAddress.Id;
            this.MerchantId = MerchantAddress.MerchantId;
            this.Code = MerchantAddress.Code;
            this.Address = MerchantAddress.Address;
            this.Contact = MerchantAddress.Contact;
            this.Phone = MerchantAddress.Phone;
        }
    }

    public class MerchantMaster_MerchantAddressFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? MerchantId { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public MerchantAddressOrder OrderBy { get; set; }
    }
}

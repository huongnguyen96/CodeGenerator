
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.merchant_address.merchant_address_master
{
    public class MerchantAddressMaster_MerchantAddressDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long MerchantId { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public MerchantAddressMaster_MerchantDTO Merchant { get; set; }
        public MerchantAddressMaster_MerchantAddressDTO() {}
        public MerchantAddressMaster_MerchantAddressDTO(MerchantAddress MerchantAddress)
        {
            
            this.Id = MerchantAddress.Id;
            this.MerchantId = MerchantAddress.MerchantId;
            this.Code = MerchantAddress.Code;
            this.Address = MerchantAddress.Address;
            this.Contact = MerchantAddress.Contact;
            this.Phone = MerchantAddress.Phone;
            this.Merchant = new MerchantAddressMaster_MerchantDTO(MerchantAddress.Merchant);

        }
    }

    public class MerchantAddressMaster_MerchantAddressFilterDTO : FilterDTO
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

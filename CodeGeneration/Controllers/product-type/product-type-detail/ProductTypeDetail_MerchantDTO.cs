
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product_type.product_type_detail
{
    public class ProductTypeDetail_MerchantDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public ProductTypeDetail_MerchantDTO() {}
        public ProductTypeDetail_MerchantDTO(Merchant Merchant)
        {
            
            this.Id = Merchant.Id;
            this.Name = Merchant.Name;
            this.Phone = Merchant.Phone;
            this.ContactPerson = Merchant.ContactPerson;
            this.Address = Merchant.Address;
        }
    }

    public class ProductTypeDetail_MerchantFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public MerchantOrder OrderBy { get; set; }
    }
}

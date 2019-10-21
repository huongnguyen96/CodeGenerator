
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.shipping_address.shipping_address_detail
{
    public class ShippingAddressDetail_DistrictDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long ProvinceId { get; set; }
        public ShippingAddressDetail_DistrictDTO() {}
        public ShippingAddressDetail_DistrictDTO(District District)
        {
            
            this.Id = District.Id;
            this.Name = District.Name;
            this.OrderNumber = District.OrderNumber;
            this.ProvinceId = District.ProvinceId;
        }
    }

    public class ShippingAddressDetail_DistrictFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public long? ProvinceId { get; set; }
    }
}

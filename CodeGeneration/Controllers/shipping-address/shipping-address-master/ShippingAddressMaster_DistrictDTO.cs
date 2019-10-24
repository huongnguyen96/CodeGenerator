
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.shipping_address.shipping_address_master
{
    public class ShippingAddressMaster_DistrictDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long ProvinceId { get; set; }
        public ShippingAddressMaster_DistrictDTO() {}
        public ShippingAddressMaster_DistrictDTO(District District)
        {
            
            this.Id = District.Id;
            this.Name = District.Name;
            this.OrderNumber = District.OrderNumber;
            this.ProvinceId = District.ProvinceId;
        }
    }

    public class ShippingAddressMaster_DistrictFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public long? ProvinceId { get; set; }
        public DistrictOrder OrderBy { get; set; }
    }
}

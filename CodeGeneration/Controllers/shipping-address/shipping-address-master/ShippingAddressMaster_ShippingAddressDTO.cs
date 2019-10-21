
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.shipping_address.shipping_address_master
{
    public class ShippingAddressMaster_ShippingAddressDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public long ProvinceId { get; set; }
        public long DistrictId { get; set; }
        public long WardId { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public ShippingAddressMaster_CustomerDTO Customer { get; set; }
        public ShippingAddressMaster_DistrictDTO District { get; set; }
        public ShippingAddressMaster_ProvinceDTO Province { get; set; }
        public ShippingAddressMaster_WardDTO Ward { get; set; }
        public ShippingAddressMaster_ShippingAddressDTO() {}
        public ShippingAddressMaster_ShippingAddressDTO(ShippingAddress ShippingAddress)
        {
            
            this.Id = ShippingAddress.Id;
            this.CustomerId = ShippingAddress.CustomerId;
            this.FullName = ShippingAddress.FullName;
            this.CompanyName = ShippingAddress.CompanyName;
            this.PhoneNumber = ShippingAddress.PhoneNumber;
            this.ProvinceId = ShippingAddress.ProvinceId;
            this.DistrictId = ShippingAddress.DistrictId;
            this.WardId = ShippingAddress.WardId;
            this.Address = ShippingAddress.Address;
            this.IsDefault = ShippingAddress.IsDefault;
            this.Customer = new ShippingAddressMaster_CustomerDTO(ShippingAddress.Customer);

            this.District = new ShippingAddressMaster_DistrictDTO(ShippingAddress.District);

            this.Province = new ShippingAddressMaster_ProvinceDTO(ShippingAddress.Province);

            this.Ward = new ShippingAddressMaster_WardDTO(ShippingAddress.Ward);

        }
    }

    public class ShippingAddressMaster_ShippingAddressFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? WardId { get; set; }
        public string Address { get; set; }
    }
}

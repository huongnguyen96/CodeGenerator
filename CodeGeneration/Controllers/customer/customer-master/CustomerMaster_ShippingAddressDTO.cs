
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.customer.customer_master
{
    public class CustomerMaster_ShippingAddressDTO : DataDTO
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
        public CustomerMaster_DistrictDTO District { get; set; }
        public CustomerMaster_ProvinceDTO Province { get; set; }
        public CustomerMaster_WardDTO Ward { get; set; }
        public CustomerMaster_ShippingAddressDTO() {}
        public CustomerMaster_ShippingAddressDTO(ShippingAddress ShippingAddress)
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
            this.District = new CustomerMaster_DistrictDTO(ShippingAddress.District);

            this.Province = new CustomerMaster_ProvinceDTO(ShippingAddress.Province);

            this.Ward = new CustomerMaster_WardDTO(ShippingAddress.Ward);

        }
    }

    public class CustomerMaster_ShippingAddressFilterDTO : FilterDTO
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

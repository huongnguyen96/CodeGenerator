
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.district.district_detail
{
    public class DistrictDetail_ShippingAddressDTO : DataDTO
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
        public DistrictDetail_CustomerDTO Customer { get; set; }
        public DistrictDetail_ProvinceDTO Province { get; set; }
        public DistrictDetail_WardDTO Ward { get; set; }
        public DistrictDetail_ShippingAddressDTO() {}
        public DistrictDetail_ShippingAddressDTO(ShippingAddress ShippingAddress)
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
            this.Customer = new DistrictDetail_CustomerDTO(ShippingAddress.Customer);

            this.Province = new DistrictDetail_ProvinceDTO(ShippingAddress.Province);

            this.Ward = new DistrictDetail_WardDTO(ShippingAddress.Ward);

        }
    }

    public class DistrictDetail_ShippingAddressFilterDTO : FilterDTO
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
        public ShippingAddressOrder OrderBy { get; set; }
    }
}

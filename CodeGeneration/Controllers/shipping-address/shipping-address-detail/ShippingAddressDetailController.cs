

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MShippingAddress;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MCustomer;
using WG.Services.MDistrict;
using WG.Services.MProvince;
using WG.Services.MWard;


namespace WG.Controllers.shipping_address.shipping_address_detail
{
    public class ShippingAddressDetailRoute : Root
    {
        public const string FE = "/shipping-address/shipping-address-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListCustomer= Default + "/single-list-customer";
        public const string SingleListDistrict= Default + "/single-list-district";
        public const string SingleListProvince= Default + "/single-list-province";
        public const string SingleListWard= Default + "/single-list-ward";
    }

    public class ShippingAddressDetailController : ApiController
    {
        
        
        private ICustomerService CustomerService;
        private IDistrictService DistrictService;
        private IProvinceService ProvinceService;
        private IWardService WardService;
        private IShippingAddressService ShippingAddressService;

        public ShippingAddressDetailController(
            
            ICustomerService CustomerService,
            IDistrictService DistrictService,
            IProvinceService ProvinceService,
            IWardService WardService,
            IShippingAddressService ShippingAddressService
        )
        {
            
            this.CustomerService = CustomerService;
            this.DistrictService = DistrictService;
            this.ProvinceService = ProvinceService;
            this.WardService = WardService;
            this.ShippingAddressService = ShippingAddressService;
        }


        [Route(ShippingAddressDetailRoute.Get), HttpPost]
        public async Task<ShippingAddressDetail_ShippingAddressDTO> Get([FromBody]ShippingAddressDetail_ShippingAddressDTO ShippingAddressDetail_ShippingAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ShippingAddress ShippingAddress = await ShippingAddressService.Get(ShippingAddressDetail_ShippingAddressDTO.Id);
            return new ShippingAddressDetail_ShippingAddressDTO(ShippingAddress);
        }


        [Route(ShippingAddressDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ShippingAddressDetail_ShippingAddressDTO>> Create([FromBody] ShippingAddressDetail_ShippingAddressDTO ShippingAddressDetail_ShippingAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ShippingAddress ShippingAddress = ConvertDTOToEntity(ShippingAddressDetail_ShippingAddressDTO);

            ShippingAddress = await ShippingAddressService.Create(ShippingAddress);
            ShippingAddressDetail_ShippingAddressDTO = new ShippingAddressDetail_ShippingAddressDTO(ShippingAddress);
            if (ShippingAddress.IsValidated)
                return ShippingAddressDetail_ShippingAddressDTO;
            else
                return BadRequest(ShippingAddressDetail_ShippingAddressDTO);        
        }

        [Route(ShippingAddressDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ShippingAddressDetail_ShippingAddressDTO>> Update([FromBody] ShippingAddressDetail_ShippingAddressDTO ShippingAddressDetail_ShippingAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ShippingAddress ShippingAddress = ConvertDTOToEntity(ShippingAddressDetail_ShippingAddressDTO);

            ShippingAddress = await ShippingAddressService.Update(ShippingAddress);
            ShippingAddressDetail_ShippingAddressDTO = new ShippingAddressDetail_ShippingAddressDTO(ShippingAddress);
            if (ShippingAddress.IsValidated)
                return ShippingAddressDetail_ShippingAddressDTO;
            else
                return BadRequest(ShippingAddressDetail_ShippingAddressDTO);        
        }

        [Route(ShippingAddressDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ShippingAddressDetail_ShippingAddressDTO>> Delete([FromBody] ShippingAddressDetail_ShippingAddressDTO ShippingAddressDetail_ShippingAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ShippingAddress ShippingAddress = ConvertDTOToEntity(ShippingAddressDetail_ShippingAddressDTO);

            ShippingAddress = await ShippingAddressService.Delete(ShippingAddress);
            ShippingAddressDetail_ShippingAddressDTO = new ShippingAddressDetail_ShippingAddressDTO(ShippingAddress);
            if (ShippingAddress.IsValidated)
                return ShippingAddressDetail_ShippingAddressDTO;
            else
                return BadRequest(ShippingAddressDetail_ShippingAddressDTO);        
        }

        public ShippingAddress ConvertDTOToEntity(ShippingAddressDetail_ShippingAddressDTO ShippingAddressDetail_ShippingAddressDTO)
        {
            ShippingAddress ShippingAddress = new ShippingAddress();
            
            ShippingAddress.Id = ShippingAddressDetail_ShippingAddressDTO.Id;
            ShippingAddress.CustomerId = ShippingAddressDetail_ShippingAddressDTO.CustomerId;
            ShippingAddress.FullName = ShippingAddressDetail_ShippingAddressDTO.FullName;
            ShippingAddress.CompanyName = ShippingAddressDetail_ShippingAddressDTO.CompanyName;
            ShippingAddress.PhoneNumber = ShippingAddressDetail_ShippingAddressDTO.PhoneNumber;
            ShippingAddress.ProvinceId = ShippingAddressDetail_ShippingAddressDTO.ProvinceId;
            ShippingAddress.DistrictId = ShippingAddressDetail_ShippingAddressDTO.DistrictId;
            ShippingAddress.WardId = ShippingAddressDetail_ShippingAddressDTO.WardId;
            ShippingAddress.Address = ShippingAddressDetail_ShippingAddressDTO.Address;
            ShippingAddress.IsDefault = ShippingAddressDetail_ShippingAddressDTO.IsDefault;
            return ShippingAddress;
        }
        
        
        [Route(ShippingAddressDetailRoute.SingleListCustomer), HttpPost]
        public async Task<List<ShippingAddressDetail_CustomerDTO>> SingleListCustomer([FromBody] ShippingAddressDetail_CustomerFilterDTO ShippingAddressDetail_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            
            CustomerFilter.Id = new LongFilter{ Equal = ShippingAddressDetail_CustomerFilterDTO.Id };
            CustomerFilter.Username = new StringFilter{ StartsWith = ShippingAddressDetail_CustomerFilterDTO.Username };
            CustomerFilter.DisplayName = new StringFilter{ StartsWith = ShippingAddressDetail_CustomerFilterDTO.DisplayName };

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<ShippingAddressDetail_CustomerDTO> ShippingAddressDetail_CustomerDTOs = Customers
                .Select(x => new ShippingAddressDetail_CustomerDTO(x)).ToList();
            return ShippingAddressDetail_CustomerDTOs;
        }

        [Route(ShippingAddressDetailRoute.SingleListDistrict), HttpPost]
        public async Task<List<ShippingAddressDetail_DistrictDTO>> SingleListDistrict([FromBody] ShippingAddressDetail_DistrictFilterDTO ShippingAddressDetail_DistrictFilterDTO)
        {
            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            
            DistrictFilter.Id = new LongFilter{ Equal = ShippingAddressDetail_DistrictFilterDTO.Id };
            DistrictFilter.Name = new StringFilter{ StartsWith = ShippingAddressDetail_DistrictFilterDTO.Name };
            DistrictFilter.OrderNumber = new LongFilter{ Equal = ShippingAddressDetail_DistrictFilterDTO.OrderNumber };
            DistrictFilter.ProvinceId = new LongFilter{ Equal = ShippingAddressDetail_DistrictFilterDTO.ProvinceId };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<ShippingAddressDetail_DistrictDTO> ShippingAddressDetail_DistrictDTOs = Districts
                .Select(x => new ShippingAddressDetail_DistrictDTO(x)).ToList();
            return ShippingAddressDetail_DistrictDTOs;
        }

        [Route(ShippingAddressDetailRoute.SingleListProvince), HttpPost]
        public async Task<List<ShippingAddressDetail_ProvinceDTO>> SingleListProvince([FromBody] ShippingAddressDetail_ProvinceFilterDTO ShippingAddressDetail_ProvinceFilterDTO)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            
            ProvinceFilter.Id = new LongFilter{ Equal = ShippingAddressDetail_ProvinceFilterDTO.Id };
            ProvinceFilter.Name = new StringFilter{ StartsWith = ShippingAddressDetail_ProvinceFilterDTO.Name };
            ProvinceFilter.OrderNumber = new LongFilter{ Equal = ShippingAddressDetail_ProvinceFilterDTO.OrderNumber };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<ShippingAddressDetail_ProvinceDTO> ShippingAddressDetail_ProvinceDTOs = Provinces
                .Select(x => new ShippingAddressDetail_ProvinceDTO(x)).ToList();
            return ShippingAddressDetail_ProvinceDTOs;
        }

        [Route(ShippingAddressDetailRoute.SingleListWard), HttpPost]
        public async Task<List<ShippingAddressDetail_WardDTO>> SingleListWard([FromBody] ShippingAddressDetail_WardFilterDTO ShippingAddressDetail_WardFilterDTO)
        {
            WardFilter WardFilter = new WardFilter();
            WardFilter.Skip = 0;
            WardFilter.Take = 20;
            WardFilter.OrderBy = WardOrder.Id;
            WardFilter.OrderType = OrderType.ASC;
            WardFilter.Selects = WardSelect.ALL;
            
            WardFilter.Id = new LongFilter{ Equal = ShippingAddressDetail_WardFilterDTO.Id };
            WardFilter.Name = new StringFilter{ StartsWith = ShippingAddressDetail_WardFilterDTO.Name };
            WardFilter.OrderNumber = new LongFilter{ Equal = ShippingAddressDetail_WardFilterDTO.OrderNumber };
            WardFilter.DistrictId = new LongFilter{ Equal = ShippingAddressDetail_WardFilterDTO.DistrictId };

            List<Ward> Wards = await WardService.List(WardFilter);
            List<ShippingAddressDetail_WardDTO> ShippingAddressDetail_WardDTOs = Wards
                .Select(x => new ShippingAddressDetail_WardDTO(x)).ToList();
            return ShippingAddressDetail_WardDTOs;
        }

    }
}

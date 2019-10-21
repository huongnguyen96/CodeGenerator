

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


namespace WG.Controllers.shipping_address.shipping_address_master
{
    public class ShippingAddressMasterRoute : Root
    {
        public const string FE = "/shipping-address/shipping-address-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListCustomer="/single-list-customer";
        public const string SingleListDistrict="/single-list-district";
        public const string SingleListProvince="/single-list-province";
        public const string SingleListWard="/single-list-ward";
    }

    public class ShippingAddressMasterController : ApiController
    {
        
        
        private ICustomerService CustomerService;
        private IDistrictService DistrictService;
        private IProvinceService ProvinceService;
        private IWardService WardService;
        private IShippingAddressService ShippingAddressService;

        public ShippingAddressMasterController(
            
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


        [Route(ShippingAddressMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ShippingAddressMaster_ShippingAddressFilterDTO ShippingAddressMaster_ShippingAddressFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ShippingAddressFilter ShippingAddressFilter = ConvertFilterDTOToFilterEntity(ShippingAddressMaster_ShippingAddressFilterDTO);

            return await ShippingAddressService.Count(ShippingAddressFilter);
        }

        [Route(ShippingAddressMasterRoute.List), HttpPost]
        public async Task<List<ShippingAddressMaster_ShippingAddressDTO>> List([FromBody] ShippingAddressMaster_ShippingAddressFilterDTO ShippingAddressMaster_ShippingAddressFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ShippingAddressFilter ShippingAddressFilter = ConvertFilterDTOToFilterEntity(ShippingAddressMaster_ShippingAddressFilterDTO);

            List<ShippingAddress> ShippingAddresss = await ShippingAddressService.List(ShippingAddressFilter);

            return ShippingAddresss.Select(c => new ShippingAddressMaster_ShippingAddressDTO(c)).ToList();
        }

        [Route(ShippingAddressMasterRoute.Get), HttpPost]
        public async Task<ShippingAddressMaster_ShippingAddressDTO> Get([FromBody]ShippingAddressMaster_ShippingAddressDTO ShippingAddressMaster_ShippingAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ShippingAddress ShippingAddress = await ShippingAddressService.Get(ShippingAddressMaster_ShippingAddressDTO.Id);
            return new ShippingAddressMaster_ShippingAddressDTO(ShippingAddress);
        }


        public ShippingAddressFilter ConvertFilterDTOToFilterEntity(ShippingAddressMaster_ShippingAddressFilterDTO ShippingAddressMaster_ShippingAddressFilterDTO)
        {
            ShippingAddressFilter ShippingAddressFilter = new ShippingAddressFilter();
            
            ShippingAddressFilter.Id = new LongFilter{ Equal = ShippingAddressMaster_ShippingAddressFilterDTO.Id };
            ShippingAddressFilter.CustomerId = new LongFilter{ Equal = ShippingAddressMaster_ShippingAddressFilterDTO.CustomerId };
            ShippingAddressFilter.FullName = new StringFilter{ StartsWith = ShippingAddressMaster_ShippingAddressFilterDTO.FullName };
            ShippingAddressFilter.CompanyName = new StringFilter{ StartsWith = ShippingAddressMaster_ShippingAddressFilterDTO.CompanyName };
            ShippingAddressFilter.PhoneNumber = new StringFilter{ StartsWith = ShippingAddressMaster_ShippingAddressFilterDTO.PhoneNumber };
            ShippingAddressFilter.ProvinceId = new LongFilter{ Equal = ShippingAddressMaster_ShippingAddressFilterDTO.ProvinceId };
            ShippingAddressFilter.DistrictId = new LongFilter{ Equal = ShippingAddressMaster_ShippingAddressFilterDTO.DistrictId };
            ShippingAddressFilter.WardId = new LongFilter{ Equal = ShippingAddressMaster_ShippingAddressFilterDTO.WardId };
            ShippingAddressFilter.Address = new StringFilter{ StartsWith = ShippingAddressMaster_ShippingAddressFilterDTO.Address };
            return ShippingAddressFilter;
        }
        
        
        [Route(ShippingAddressMasterRoute.SingleListCustomer), HttpPost]
        public async Task<List<ShippingAddressMaster_CustomerDTO>> SingleListCustomer([FromBody] ShippingAddressMaster_CustomerFilterDTO ShippingAddressMaster_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            
            CustomerFilter.Id = new LongFilter{ Equal = ShippingAddressMaster_CustomerFilterDTO.Id };
            CustomerFilter.Username = new StringFilter{ StartsWith = ShippingAddressMaster_CustomerFilterDTO.Username };
            CustomerFilter.DisplayName = new StringFilter{ StartsWith = ShippingAddressMaster_CustomerFilterDTO.DisplayName };

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<ShippingAddressMaster_CustomerDTO> ShippingAddressMaster_CustomerDTOs = Customers
                .Select(x => new ShippingAddressMaster_CustomerDTO(x)).ToList();
            return ShippingAddressMaster_CustomerDTOs;
        }

        [Route(ShippingAddressMasterRoute.SingleListDistrict), HttpPost]
        public async Task<List<ShippingAddressMaster_DistrictDTO>> SingleListDistrict([FromBody] ShippingAddressMaster_DistrictFilterDTO ShippingAddressMaster_DistrictFilterDTO)
        {
            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            
            DistrictFilter.Id = new LongFilter{ Equal = ShippingAddressMaster_DistrictFilterDTO.Id };
            DistrictFilter.Name = new StringFilter{ StartsWith = ShippingAddressMaster_DistrictFilterDTO.Name };
            DistrictFilter.OrderNumber = new LongFilter{ Equal = ShippingAddressMaster_DistrictFilterDTO.OrderNumber };
            DistrictFilter.ProvinceId = new LongFilter{ Equal = ShippingAddressMaster_DistrictFilterDTO.ProvinceId };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<ShippingAddressMaster_DistrictDTO> ShippingAddressMaster_DistrictDTOs = Districts
                .Select(x => new ShippingAddressMaster_DistrictDTO(x)).ToList();
            return ShippingAddressMaster_DistrictDTOs;
        }

        [Route(ShippingAddressMasterRoute.SingleListProvince), HttpPost]
        public async Task<List<ShippingAddressMaster_ProvinceDTO>> SingleListProvince([FromBody] ShippingAddressMaster_ProvinceFilterDTO ShippingAddressMaster_ProvinceFilterDTO)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            
            ProvinceFilter.Id = new LongFilter{ Equal = ShippingAddressMaster_ProvinceFilterDTO.Id };
            ProvinceFilter.Name = new StringFilter{ StartsWith = ShippingAddressMaster_ProvinceFilterDTO.Name };
            ProvinceFilter.OrderNumber = new LongFilter{ Equal = ShippingAddressMaster_ProvinceFilterDTO.OrderNumber };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<ShippingAddressMaster_ProvinceDTO> ShippingAddressMaster_ProvinceDTOs = Provinces
                .Select(x => new ShippingAddressMaster_ProvinceDTO(x)).ToList();
            return ShippingAddressMaster_ProvinceDTOs;
        }

        [Route(ShippingAddressMasterRoute.SingleListWard), HttpPost]
        public async Task<List<ShippingAddressMaster_WardDTO>> SingleListWard([FromBody] ShippingAddressMaster_WardFilterDTO ShippingAddressMaster_WardFilterDTO)
        {
            WardFilter WardFilter = new WardFilter();
            WardFilter.Skip = 0;
            WardFilter.Take = 20;
            WardFilter.OrderBy = WardOrder.Id;
            WardFilter.OrderType = OrderType.ASC;
            WardFilter.Selects = WardSelect.ALL;
            
            WardFilter.Id = new LongFilter{ Equal = ShippingAddressMaster_WardFilterDTO.Id };
            WardFilter.Name = new StringFilter{ StartsWith = ShippingAddressMaster_WardFilterDTO.Name };
            WardFilter.OrderNumber = new LongFilter{ Equal = ShippingAddressMaster_WardFilterDTO.OrderNumber };
            WardFilter.DistrictId = new LongFilter{ Equal = ShippingAddressMaster_WardFilterDTO.DistrictId };

            List<Ward> Wards = await WardService.List(WardFilter);
            List<ShippingAddressMaster_WardDTO> ShippingAddressMaster_WardDTOs = Wards
                .Select(x => new ShippingAddressMaster_WardDTO(x)).ToList();
            return ShippingAddressMaster_WardDTOs;
        }

    }
}

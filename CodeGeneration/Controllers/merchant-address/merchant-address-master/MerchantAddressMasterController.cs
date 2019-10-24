

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MMerchantAddress;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MMerchant;


namespace WG.Controllers.merchant_address.merchant_address_master
{
    public class MerchantAddressMasterRoute : Root
    {
        public const string FE = "/merchant-address/merchant-address-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListMerchant= Default + "/single-list-merchant";
    }

    public class MerchantAddressMasterController : ApiController
    {
        
        
        private IMerchantService MerchantService;
        private IMerchantAddressService MerchantAddressService;

        public MerchantAddressMasterController(
            
            IMerchantService MerchantService,
            IMerchantAddressService MerchantAddressService
        )
        {
            
            this.MerchantService = MerchantService;
            this.MerchantAddressService = MerchantAddressService;
        }


        [Route(MerchantAddressMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] MerchantAddressMaster_MerchantAddressFilterDTO MerchantAddressMaster_MerchantAddressFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantAddressFilter MerchantAddressFilter = ConvertFilterDTOToFilterEntity(MerchantAddressMaster_MerchantAddressFilterDTO);

            return await MerchantAddressService.Count(MerchantAddressFilter);
        }

        [Route(MerchantAddressMasterRoute.List), HttpPost]
        public async Task<List<MerchantAddressMaster_MerchantAddressDTO>> List([FromBody] MerchantAddressMaster_MerchantAddressFilterDTO MerchantAddressMaster_MerchantAddressFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantAddressFilter MerchantAddressFilter = ConvertFilterDTOToFilterEntity(MerchantAddressMaster_MerchantAddressFilterDTO);

            List<MerchantAddress> MerchantAddresss = await MerchantAddressService.List(MerchantAddressFilter);

            return MerchantAddresss.Select(c => new MerchantAddressMaster_MerchantAddressDTO(c)).ToList();
        }

        [Route(MerchantAddressMasterRoute.Get), HttpPost]
        public async Task<MerchantAddressMaster_MerchantAddressDTO> Get([FromBody]MerchantAddressMaster_MerchantAddressDTO MerchantAddressMaster_MerchantAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantAddress MerchantAddress = await MerchantAddressService.Get(MerchantAddressMaster_MerchantAddressDTO.Id);
            return new MerchantAddressMaster_MerchantAddressDTO(MerchantAddress);
        }


        public MerchantAddressFilter ConvertFilterDTOToFilterEntity(MerchantAddressMaster_MerchantAddressFilterDTO MerchantAddressMaster_MerchantAddressFilterDTO)
        {
            MerchantAddressFilter MerchantAddressFilter = new MerchantAddressFilter();
            MerchantAddressFilter.Selects = MerchantAddressSelect.ALL;
            
            MerchantAddressFilter.Id = new LongFilter{ Equal = MerchantAddressMaster_MerchantAddressFilterDTO.Id };
            MerchantAddressFilter.MerchantId = new LongFilter{ Equal = MerchantAddressMaster_MerchantAddressFilterDTO.MerchantId };
            MerchantAddressFilter.Code = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantAddressFilterDTO.Code };
            MerchantAddressFilter.Address = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantAddressFilterDTO.Address };
            MerchantAddressFilter.Contact = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantAddressFilterDTO.Contact };
            MerchantAddressFilter.Phone = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantAddressFilterDTO.Phone };
            return MerchantAddressFilter;
        }
        
        
        [Route(MerchantAddressMasterRoute.SingleListMerchant), HttpPost]
        public async Task<List<MerchantAddressMaster_MerchantDTO>> SingleListMerchant([FromBody] MerchantAddressMaster_MerchantFilterDTO MerchantAddressMaster_MerchantFilterDTO)
        {
            MerchantFilter MerchantFilter = new MerchantFilter();
            MerchantFilter.Skip = 0;
            MerchantFilter.Take = 20;
            MerchantFilter.OrderBy = MerchantOrder.Id;
            MerchantFilter.OrderType = OrderType.ASC;
            MerchantFilter.Selects = MerchantSelect.ALL;
            
            MerchantFilter.Id = new LongFilter{ Equal = MerchantAddressMaster_MerchantFilterDTO.Id };
            MerchantFilter.Name = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantFilterDTO.Name };
            MerchantFilter.Phone = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantFilterDTO.Phone };
            MerchantFilter.ContactPerson = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantFilterDTO.ContactPerson };
            MerchantFilter.Address = new StringFilter{ StartsWith = MerchantAddressMaster_MerchantFilterDTO.Address };

            List<Merchant> Merchants = await MerchantService.List(MerchantFilter);
            List<MerchantAddressMaster_MerchantDTO> MerchantAddressMaster_MerchantDTOs = Merchants
                .Select(x => new MerchantAddressMaster_MerchantDTO(x)).ToList();
            return MerchantAddressMaster_MerchantDTOs;
        }

    }
}

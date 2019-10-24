

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MMerchant;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.merchant.merchant_master
{
    public class MerchantMasterRoute : Root
    {
        public const string FE = "/merchant/merchant-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class MerchantMasterController : ApiController
    {
        
        
        private IMerchantService MerchantService;

        public MerchantMasterController(
            
            IMerchantService MerchantService
        )
        {
            
            this.MerchantService = MerchantService;
        }


        [Route(MerchantMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] MerchantMaster_MerchantFilterDTO MerchantMaster_MerchantFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantFilter MerchantFilter = ConvertFilterDTOToFilterEntity(MerchantMaster_MerchantFilterDTO);

            return await MerchantService.Count(MerchantFilter);
        }

        [Route(MerchantMasterRoute.List), HttpPost]
        public async Task<List<MerchantMaster_MerchantDTO>> List([FromBody] MerchantMaster_MerchantFilterDTO MerchantMaster_MerchantFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantFilter MerchantFilter = ConvertFilterDTOToFilterEntity(MerchantMaster_MerchantFilterDTO);

            List<Merchant> Merchants = await MerchantService.List(MerchantFilter);

            return Merchants.Select(c => new MerchantMaster_MerchantDTO(c)).ToList();
        }

        [Route(MerchantMasterRoute.Get), HttpPost]
        public async Task<MerchantMaster_MerchantDTO> Get([FromBody]MerchantMaster_MerchantDTO MerchantMaster_MerchantDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Merchant Merchant = await MerchantService.Get(MerchantMaster_MerchantDTO.Id);
            return new MerchantMaster_MerchantDTO(Merchant);
        }


        public MerchantFilter ConvertFilterDTOToFilterEntity(MerchantMaster_MerchantFilterDTO MerchantMaster_MerchantFilterDTO)
        {
            MerchantFilter MerchantFilter = new MerchantFilter();
            MerchantFilter.Selects = MerchantSelect.ALL;
            
            MerchantFilter.Id = new LongFilter{ Equal = MerchantMaster_MerchantFilterDTO.Id };
            MerchantFilter.Name = new StringFilter{ StartsWith = MerchantMaster_MerchantFilterDTO.Name };
            MerchantFilter.Phone = new StringFilter{ StartsWith = MerchantMaster_MerchantFilterDTO.Phone };
            MerchantFilter.ContactPerson = new StringFilter{ StartsWith = MerchantMaster_MerchantFilterDTO.ContactPerson };
            MerchantFilter.Address = new StringFilter{ StartsWith = MerchantMaster_MerchantFilterDTO.Address };
            return MerchantFilter;
        }
        
        
    }
}

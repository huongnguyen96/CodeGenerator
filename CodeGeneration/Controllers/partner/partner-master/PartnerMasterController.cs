

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MPartner;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.partner.partner_master
{
    public class PartnerMasterRoute : Root
    {
        public const string FE = "/partner/partner-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class PartnerMasterController : ApiController
    {
        
        
        private IPartnerService PartnerService;

        public PartnerMasterController(
            
            IPartnerService PartnerService
        )
        {
            
            this.PartnerService = PartnerService;
        }


        [Route(PartnerMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] PartnerMaster_PartnerFilterDTO PartnerMaster_PartnerFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PartnerFilter PartnerFilter = ConvertFilterDTOToFilterEntity(PartnerMaster_PartnerFilterDTO);

            return await PartnerService.Count(PartnerFilter);
        }

        [Route(PartnerMasterRoute.List), HttpPost]
        public async Task<List<PartnerMaster_PartnerDTO>> List([FromBody] PartnerMaster_PartnerFilterDTO PartnerMaster_PartnerFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            PartnerFilter PartnerFilter = ConvertFilterDTOToFilterEntity(PartnerMaster_PartnerFilterDTO);

            List<Partner> Partners = await PartnerService.List(PartnerFilter);

            return Partners.Select(c => new PartnerMaster_PartnerDTO(c)).ToList();
        }

        [Route(PartnerMasterRoute.Get), HttpPost]
        public async Task<PartnerMaster_PartnerDTO> Get([FromBody]PartnerMaster_PartnerDTO PartnerMaster_PartnerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Partner Partner = await PartnerService.Get(PartnerMaster_PartnerDTO.Id);
            return new PartnerMaster_PartnerDTO(Partner);
        }


        public PartnerFilter ConvertFilterDTOToFilterEntity(PartnerMaster_PartnerFilterDTO PartnerMaster_PartnerFilterDTO)
        {
            PartnerFilter PartnerFilter = new PartnerFilter();
            
            PartnerFilter.Id = new LongFilter{ Equal = PartnerMaster_PartnerFilterDTO.Id };
            PartnerFilter.Name = new StringFilter{ StartsWith = PartnerMaster_PartnerFilterDTO.Name };
            PartnerFilter.Phone = new StringFilter{ StartsWith = PartnerMaster_PartnerFilterDTO.Phone };
            PartnerFilter.ContactPerson = new StringFilter{ StartsWith = PartnerMaster_PartnerFilterDTO.ContactPerson };
            PartnerFilter.Address = new StringFilter{ StartsWith = PartnerMaster_PartnerFilterDTO.Address };
            return PartnerFilter;
        }
        
        
    }
}

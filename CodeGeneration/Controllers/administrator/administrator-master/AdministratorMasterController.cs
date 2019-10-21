

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MAdministrator;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.administrator.administrator_master
{
    public class AdministratorMasterRoute : Root
    {
        public const string FE = "/administrator/administrator-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class AdministratorMasterController : ApiController
    {
        
        
        private IAdministratorService AdministratorService;

        public AdministratorMasterController(
            
            IAdministratorService AdministratorService
        )
        {
            
            this.AdministratorService = AdministratorService;
        }


        [Route(AdministratorMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] AdministratorMaster_AdministratorFilterDTO AdministratorMaster_AdministratorFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            AdministratorFilter AdministratorFilter = ConvertFilterDTOToFilterEntity(AdministratorMaster_AdministratorFilterDTO);

            return await AdministratorService.Count(AdministratorFilter);
        }

        [Route(AdministratorMasterRoute.List), HttpPost]
        public async Task<List<AdministratorMaster_AdministratorDTO>> List([FromBody] AdministratorMaster_AdministratorFilterDTO AdministratorMaster_AdministratorFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            AdministratorFilter AdministratorFilter = ConvertFilterDTOToFilterEntity(AdministratorMaster_AdministratorFilterDTO);

            List<Administrator> Administrators = await AdministratorService.List(AdministratorFilter);

            return Administrators.Select(c => new AdministratorMaster_AdministratorDTO(c)).ToList();
        }

        [Route(AdministratorMasterRoute.Get), HttpPost]
        public async Task<AdministratorMaster_AdministratorDTO> Get([FromBody]AdministratorMaster_AdministratorDTO AdministratorMaster_AdministratorDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Administrator Administrator = await AdministratorService.Get(AdministratorMaster_AdministratorDTO.Id);
            return new AdministratorMaster_AdministratorDTO(Administrator);
        }


        public AdministratorFilter ConvertFilterDTOToFilterEntity(AdministratorMaster_AdministratorFilterDTO AdministratorMaster_AdministratorFilterDTO)
        {
            AdministratorFilter AdministratorFilter = new AdministratorFilter();
            
            AdministratorFilter.Id = new LongFilter{ Equal = AdministratorMaster_AdministratorFilterDTO.Id };
            AdministratorFilter.Username = new StringFilter{ StartsWith = AdministratorMaster_AdministratorFilterDTO.Username };
            AdministratorFilter.DisplayName = new StringFilter{ StartsWith = AdministratorMaster_AdministratorFilterDTO.DisplayName };
            return AdministratorFilter;
        }
        
        
    }
}

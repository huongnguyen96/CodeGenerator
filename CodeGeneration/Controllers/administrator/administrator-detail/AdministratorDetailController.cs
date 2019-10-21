

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MAdministrator;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.administrator.administrator_detail
{
    public class AdministratorDetailRoute : Root
    {
        public const string FE = "/administrator/administrator-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class AdministratorDetailController : ApiController
    {
        
        
        private IAdministratorService AdministratorService;

        public AdministratorDetailController(
            
            IAdministratorService AdministratorService
        )
        {
            
            this.AdministratorService = AdministratorService;
        }


        [Route(AdministratorDetailRoute.Get), HttpPost]
        public async Task<AdministratorDetail_AdministratorDTO> Get([FromBody]AdministratorDetail_AdministratorDTO AdministratorDetail_AdministratorDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Administrator Administrator = await AdministratorService.Get(AdministratorDetail_AdministratorDTO.Id);
            return new AdministratorDetail_AdministratorDTO(Administrator);
        }


        [Route(AdministratorDetailRoute.Create), HttpPost]
        public async Task<ActionResult<AdministratorDetail_AdministratorDTO>> Create([FromBody] AdministratorDetail_AdministratorDTO AdministratorDetail_AdministratorDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Administrator Administrator = ConvertDTOToEntity(AdministratorDetail_AdministratorDTO);

            Administrator = await AdministratorService.Create(Administrator);
            AdministratorDetail_AdministratorDTO = new AdministratorDetail_AdministratorDTO(Administrator);
            if (Administrator.IsValidated)
                return AdministratorDetail_AdministratorDTO;
            else
                return BadRequest(AdministratorDetail_AdministratorDTO);        
        }

        [Route(AdministratorDetailRoute.Update), HttpPost]
        public async Task<ActionResult<AdministratorDetail_AdministratorDTO>> Update([FromBody] AdministratorDetail_AdministratorDTO AdministratorDetail_AdministratorDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Administrator Administrator = ConvertDTOToEntity(AdministratorDetail_AdministratorDTO);

            Administrator = await AdministratorService.Update(Administrator);
            AdministratorDetail_AdministratorDTO = new AdministratorDetail_AdministratorDTO(Administrator);
            if (Administrator.IsValidated)
                return AdministratorDetail_AdministratorDTO;
            else
                return BadRequest(AdministratorDetail_AdministratorDTO);        
        }

        [Route(AdministratorDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<AdministratorDetail_AdministratorDTO>> Delete([FromBody] AdministratorDetail_AdministratorDTO AdministratorDetail_AdministratorDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Administrator Administrator = ConvertDTOToEntity(AdministratorDetail_AdministratorDTO);

            Administrator = await AdministratorService.Delete(Administrator);
            AdministratorDetail_AdministratorDTO = new AdministratorDetail_AdministratorDTO(Administrator);
            if (Administrator.IsValidated)
                return AdministratorDetail_AdministratorDTO;
            else
                return BadRequest(AdministratorDetail_AdministratorDTO);        
        }

        public Administrator ConvertDTOToEntity(AdministratorDetail_AdministratorDTO AdministratorDetail_AdministratorDTO)
        {
            Administrator Administrator = new Administrator();
            
            Administrator.Id = AdministratorDetail_AdministratorDTO.Id;
            Administrator.Username = AdministratorDetail_AdministratorDTO.Username;
            Administrator.DisplayName = AdministratorDetail_AdministratorDTO.DisplayName;
            return Administrator;
        }
        
        
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MPartner;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.partner.partner_detail
{
    public class PartnerDetailRoute : Root
    {
        public const string FE = "/partner/partner-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class PartnerDetailController : ApiController
    {
        
        
        private IPartnerService PartnerService;

        public PartnerDetailController(
            
            IPartnerService PartnerService
        )
        {
            
            this.PartnerService = PartnerService;
        }


        [Route(PartnerDetailRoute.Get), HttpPost]
        public async Task<PartnerDetail_PartnerDTO> Get([FromBody]PartnerDetail_PartnerDTO PartnerDetail_PartnerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Partner Partner = await PartnerService.Get(PartnerDetail_PartnerDTO.Id);
            return new PartnerDetail_PartnerDTO(Partner);
        }


        [Route(PartnerDetailRoute.Create), HttpPost]
        public async Task<ActionResult<PartnerDetail_PartnerDTO>> Create([FromBody] PartnerDetail_PartnerDTO PartnerDetail_PartnerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Partner Partner = ConvertDTOToEntity(PartnerDetail_PartnerDTO);

            Partner = await PartnerService.Create(Partner);
            PartnerDetail_PartnerDTO = new PartnerDetail_PartnerDTO(Partner);
            if (Partner.IsValidated)
                return PartnerDetail_PartnerDTO;
            else
                return BadRequest(PartnerDetail_PartnerDTO);        
        }

        [Route(PartnerDetailRoute.Update), HttpPost]
        public async Task<ActionResult<PartnerDetail_PartnerDTO>> Update([FromBody] PartnerDetail_PartnerDTO PartnerDetail_PartnerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Partner Partner = ConvertDTOToEntity(PartnerDetail_PartnerDTO);

            Partner = await PartnerService.Update(Partner);
            PartnerDetail_PartnerDTO = new PartnerDetail_PartnerDTO(Partner);
            if (Partner.IsValidated)
                return PartnerDetail_PartnerDTO;
            else
                return BadRequest(PartnerDetail_PartnerDTO);        
        }

        [Route(PartnerDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<PartnerDetail_PartnerDTO>> Delete([FromBody] PartnerDetail_PartnerDTO PartnerDetail_PartnerDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Partner Partner = ConvertDTOToEntity(PartnerDetail_PartnerDTO);

            Partner = await PartnerService.Delete(Partner);
            PartnerDetail_PartnerDTO = new PartnerDetail_PartnerDTO(Partner);
            if (Partner.IsValidated)
                return PartnerDetail_PartnerDTO;
            else
                return BadRequest(PartnerDetail_PartnerDTO);        
        }

        public Partner ConvertDTOToEntity(PartnerDetail_PartnerDTO PartnerDetail_PartnerDTO)
        {
            Partner Partner = new Partner();
            
            Partner.Id = PartnerDetail_PartnerDTO.Id;
            Partner.Name = PartnerDetail_PartnerDTO.Name;
            Partner.Phone = PartnerDetail_PartnerDTO.Phone;
            Partner.ContactPerson = PartnerDetail_PartnerDTO.ContactPerson;
            Partner.Address = PartnerDetail_PartnerDTO.Address;
            return Partner;
        }
        
        
    }
}

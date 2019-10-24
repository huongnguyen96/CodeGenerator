

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MMerchant;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.merchant.merchant_detail
{
    public class MerchantDetailRoute : Root
    {
        public const string FE = "/merchant/merchant-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class MerchantDetailController : ApiController
    {
        
        
        private IMerchantService MerchantService;

        public MerchantDetailController(
            
            IMerchantService MerchantService
        )
        {
            
            this.MerchantService = MerchantService;
        }


        [Route(MerchantDetailRoute.Get), HttpPost]
        public async Task<MerchantDetail_MerchantDTO> Get([FromBody]MerchantDetail_MerchantDTO MerchantDetail_MerchantDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Merchant Merchant = await MerchantService.Get(MerchantDetail_MerchantDTO.Id);
            return new MerchantDetail_MerchantDTO(Merchant);
        }


        [Route(MerchantDetailRoute.Create), HttpPost]
        public async Task<ActionResult<MerchantDetail_MerchantDTO>> Create([FromBody] MerchantDetail_MerchantDTO MerchantDetail_MerchantDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Merchant Merchant = ConvertDTOToEntity(MerchantDetail_MerchantDTO);

            Merchant = await MerchantService.Create(Merchant);
            MerchantDetail_MerchantDTO = new MerchantDetail_MerchantDTO(Merchant);
            if (Merchant.IsValidated)
                return MerchantDetail_MerchantDTO;
            else
                return BadRequest(MerchantDetail_MerchantDTO);        
        }

        [Route(MerchantDetailRoute.Update), HttpPost]
        public async Task<ActionResult<MerchantDetail_MerchantDTO>> Update([FromBody] MerchantDetail_MerchantDTO MerchantDetail_MerchantDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Merchant Merchant = ConvertDTOToEntity(MerchantDetail_MerchantDTO);

            Merchant = await MerchantService.Update(Merchant);
            MerchantDetail_MerchantDTO = new MerchantDetail_MerchantDTO(Merchant);
            if (Merchant.IsValidated)
                return MerchantDetail_MerchantDTO;
            else
                return BadRequest(MerchantDetail_MerchantDTO);        
        }

        [Route(MerchantDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<MerchantDetail_MerchantDTO>> Delete([FromBody] MerchantDetail_MerchantDTO MerchantDetail_MerchantDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Merchant Merchant = ConvertDTOToEntity(MerchantDetail_MerchantDTO);

            Merchant = await MerchantService.Delete(Merchant);
            MerchantDetail_MerchantDTO = new MerchantDetail_MerchantDTO(Merchant);
            if (Merchant.IsValidated)
                return MerchantDetail_MerchantDTO;
            else
                return BadRequest(MerchantDetail_MerchantDTO);        
        }

        public Merchant ConvertDTOToEntity(MerchantDetail_MerchantDTO MerchantDetail_MerchantDTO)
        {
            Merchant Merchant = new Merchant();
            
            Merchant.Id = MerchantDetail_MerchantDTO.Id;
            Merchant.Name = MerchantDetail_MerchantDTO.Name;
            Merchant.Phone = MerchantDetail_MerchantDTO.Phone;
            Merchant.ContactPerson = MerchantDetail_MerchantDTO.ContactPerson;
            Merchant.Address = MerchantDetail_MerchantDTO.Address;
            return Merchant;
        }
        
        
    }
}

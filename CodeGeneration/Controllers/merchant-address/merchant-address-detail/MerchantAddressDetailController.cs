

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MMerchantAddress;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MMerchant;


namespace WG.Controllers.merchant_address.merchant_address_detail
{
    public class MerchantAddressDetailRoute : Root
    {
        public const string FE = "/merchant-address/merchant-address-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListMerchant= Default + "/single-list-merchant";
    }

    public class MerchantAddressDetailController : ApiController
    {
        
        
        private IMerchantService MerchantService;
        private IMerchantAddressService MerchantAddressService;

        public MerchantAddressDetailController(
            
            IMerchantService MerchantService,
            IMerchantAddressService MerchantAddressService
        )
        {
            
            this.MerchantService = MerchantService;
            this.MerchantAddressService = MerchantAddressService;
        }


        [Route(MerchantAddressDetailRoute.Get), HttpPost]
        public async Task<MerchantAddressDetail_MerchantAddressDTO> Get([FromBody]MerchantAddressDetail_MerchantAddressDTO MerchantAddressDetail_MerchantAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantAddress MerchantAddress = await MerchantAddressService.Get(MerchantAddressDetail_MerchantAddressDTO.Id);
            return new MerchantAddressDetail_MerchantAddressDTO(MerchantAddress);
        }


        [Route(MerchantAddressDetailRoute.Create), HttpPost]
        public async Task<ActionResult<MerchantAddressDetail_MerchantAddressDTO>> Create([FromBody] MerchantAddressDetail_MerchantAddressDTO MerchantAddressDetail_MerchantAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantAddress MerchantAddress = ConvertDTOToEntity(MerchantAddressDetail_MerchantAddressDTO);

            MerchantAddress = await MerchantAddressService.Create(MerchantAddress);
            MerchantAddressDetail_MerchantAddressDTO = new MerchantAddressDetail_MerchantAddressDTO(MerchantAddress);
            if (MerchantAddress.IsValidated)
                return MerchantAddressDetail_MerchantAddressDTO;
            else
                return BadRequest(MerchantAddressDetail_MerchantAddressDTO);        
        }

        [Route(MerchantAddressDetailRoute.Update), HttpPost]
        public async Task<ActionResult<MerchantAddressDetail_MerchantAddressDTO>> Update([FromBody] MerchantAddressDetail_MerchantAddressDTO MerchantAddressDetail_MerchantAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantAddress MerchantAddress = ConvertDTOToEntity(MerchantAddressDetail_MerchantAddressDTO);

            MerchantAddress = await MerchantAddressService.Update(MerchantAddress);
            MerchantAddressDetail_MerchantAddressDTO = new MerchantAddressDetail_MerchantAddressDTO(MerchantAddress);
            if (MerchantAddress.IsValidated)
                return MerchantAddressDetail_MerchantAddressDTO;
            else
                return BadRequest(MerchantAddressDetail_MerchantAddressDTO);        
        }

        [Route(MerchantAddressDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<MerchantAddressDetail_MerchantAddressDTO>> Delete([FromBody] MerchantAddressDetail_MerchantAddressDTO MerchantAddressDetail_MerchantAddressDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            MerchantAddress MerchantAddress = ConvertDTOToEntity(MerchantAddressDetail_MerchantAddressDTO);

            MerchantAddress = await MerchantAddressService.Delete(MerchantAddress);
            MerchantAddressDetail_MerchantAddressDTO = new MerchantAddressDetail_MerchantAddressDTO(MerchantAddress);
            if (MerchantAddress.IsValidated)
                return MerchantAddressDetail_MerchantAddressDTO;
            else
                return BadRequest(MerchantAddressDetail_MerchantAddressDTO);        
        }

        public MerchantAddress ConvertDTOToEntity(MerchantAddressDetail_MerchantAddressDTO MerchantAddressDetail_MerchantAddressDTO)
        {
            MerchantAddress MerchantAddress = new MerchantAddress();
            
            MerchantAddress.Id = MerchantAddressDetail_MerchantAddressDTO.Id;
            MerchantAddress.MerchantId = MerchantAddressDetail_MerchantAddressDTO.MerchantId;
            MerchantAddress.Code = MerchantAddressDetail_MerchantAddressDTO.Code;
            MerchantAddress.Address = MerchantAddressDetail_MerchantAddressDTO.Address;
            MerchantAddress.Contact = MerchantAddressDetail_MerchantAddressDTO.Contact;
            MerchantAddress.Phone = MerchantAddressDetail_MerchantAddressDTO.Phone;
            return MerchantAddress;
        }
        
        
        [Route(MerchantAddressDetailRoute.SingleListMerchant), HttpPost]
        public async Task<List<MerchantAddressDetail_MerchantDTO>> SingleListMerchant([FromBody] MerchantAddressDetail_MerchantFilterDTO MerchantAddressDetail_MerchantFilterDTO)
        {
            MerchantFilter MerchantFilter = new MerchantFilter();
            MerchantFilter.Skip = 0;
            MerchantFilter.Take = 20;
            MerchantFilter.OrderBy = MerchantOrder.Id;
            MerchantFilter.OrderType = OrderType.ASC;
            MerchantFilter.Selects = MerchantSelect.ALL;
            
            MerchantFilter.Id = new LongFilter{ Equal = MerchantAddressDetail_MerchantFilterDTO.Id };
            MerchantFilter.Name = new StringFilter{ StartsWith = MerchantAddressDetail_MerchantFilterDTO.Name };
            MerchantFilter.Phone = new StringFilter{ StartsWith = MerchantAddressDetail_MerchantFilterDTO.Phone };
            MerchantFilter.ContactPerson = new StringFilter{ StartsWith = MerchantAddressDetail_MerchantFilterDTO.ContactPerson };
            MerchantFilter.Address = new StringFilter{ StartsWith = MerchantAddressDetail_MerchantFilterDTO.Address };

            List<Merchant> Merchants = await MerchantService.List(MerchantFilter);
            List<MerchantAddressDetail_MerchantDTO> MerchantAddressDetail_MerchantDTOs = Merchants
                .Select(x => new MerchantAddressDetail_MerchantDTO(x)).ToList();
            return MerchantAddressDetail_MerchantDTOs;
        }

    }
}

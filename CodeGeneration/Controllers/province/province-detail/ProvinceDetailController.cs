

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MProvince;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.province.province_detail
{
    public class ProvinceDetailRoute : Root
    {
        public const string FE = "/province/province-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ProvinceDetailController : ApiController
    {
        
        
        private IProvinceService ProvinceService;

        public ProvinceDetailController(
            
            IProvinceService ProvinceService
        )
        {
            
            this.ProvinceService = ProvinceService;
        }


        [Route(ProvinceDetailRoute.Get), HttpPost]
        public async Task<ProvinceDetail_ProvinceDTO> Get([FromBody]ProvinceDetail_ProvinceDTO ProvinceDetail_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Province Province = await ProvinceService.Get(ProvinceDetail_ProvinceDTO.Id);
            return new ProvinceDetail_ProvinceDTO(Province);
        }


        [Route(ProvinceDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ProvinceDetail_ProvinceDTO>> Create([FromBody] ProvinceDetail_ProvinceDTO ProvinceDetail_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Province Province = ConvertDTOToEntity(ProvinceDetail_ProvinceDTO);

            Province = await ProvinceService.Create(Province);
            ProvinceDetail_ProvinceDTO = new ProvinceDetail_ProvinceDTO(Province);
            if (Province.IsValidated)
                return ProvinceDetail_ProvinceDTO;
            else
                return BadRequest(ProvinceDetail_ProvinceDTO);        
        }

        [Route(ProvinceDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ProvinceDetail_ProvinceDTO>> Update([FromBody] ProvinceDetail_ProvinceDTO ProvinceDetail_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Province Province = ConvertDTOToEntity(ProvinceDetail_ProvinceDTO);

            Province = await ProvinceService.Update(Province);
            ProvinceDetail_ProvinceDTO = new ProvinceDetail_ProvinceDTO(Province);
            if (Province.IsValidated)
                return ProvinceDetail_ProvinceDTO;
            else
                return BadRequest(ProvinceDetail_ProvinceDTO);        
        }

        [Route(ProvinceDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ProvinceDetail_ProvinceDTO>> Delete([FromBody] ProvinceDetail_ProvinceDTO ProvinceDetail_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Province Province = ConvertDTOToEntity(ProvinceDetail_ProvinceDTO);

            Province = await ProvinceService.Delete(Province);
            ProvinceDetail_ProvinceDTO = new ProvinceDetail_ProvinceDTO(Province);
            if (Province.IsValidated)
                return ProvinceDetail_ProvinceDTO;
            else
                return BadRequest(ProvinceDetail_ProvinceDTO);        
        }

        public Province ConvertDTOToEntity(ProvinceDetail_ProvinceDTO ProvinceDetail_ProvinceDTO)
        {
            Province Province = new Province();
            
            Province.Id = ProvinceDetail_ProvinceDTO.Id;
            Province.Name = ProvinceDetail_ProvinceDTO.Name;
            Province.OrderNumber = ProvinceDetail_ProvinceDTO.OrderNumber;
            return Province;
        }
        
        
    }
}

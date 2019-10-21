

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDistrict;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MProvince;


namespace WG.Controllers.district.district_detail
{
    public class DistrictDetailRoute : Root
    {
        public const string FE = "/district/district-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListProvince="/single-list-province";
    }

    public class DistrictDetailController : ApiController
    {
        
        
        private IProvinceService ProvinceService;
        private IDistrictService DistrictService;

        public DistrictDetailController(
            
            IProvinceService ProvinceService,
            IDistrictService DistrictService
        )
        {
            
            this.ProvinceService = ProvinceService;
            this.DistrictService = DistrictService;
        }


        [Route(DistrictDetailRoute.Get), HttpPost]
        public async Task<DistrictDetail_DistrictDTO> Get([FromBody]DistrictDetail_DistrictDTO DistrictDetail_DistrictDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            District District = await DistrictService.Get(DistrictDetail_DistrictDTO.Id);
            return new DistrictDetail_DistrictDTO(District);
        }


        [Route(DistrictDetailRoute.Create), HttpPost]
        public async Task<ActionResult<DistrictDetail_DistrictDTO>> Create([FromBody] DistrictDetail_DistrictDTO DistrictDetail_DistrictDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            District District = ConvertDTOToEntity(DistrictDetail_DistrictDTO);

            District = await DistrictService.Create(District);
            DistrictDetail_DistrictDTO = new DistrictDetail_DistrictDTO(District);
            if (District.IsValidated)
                return DistrictDetail_DistrictDTO;
            else
                return BadRequest(DistrictDetail_DistrictDTO);        
        }

        [Route(DistrictDetailRoute.Update), HttpPost]
        public async Task<ActionResult<DistrictDetail_DistrictDTO>> Update([FromBody] DistrictDetail_DistrictDTO DistrictDetail_DistrictDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            District District = ConvertDTOToEntity(DistrictDetail_DistrictDTO);

            District = await DistrictService.Update(District);
            DistrictDetail_DistrictDTO = new DistrictDetail_DistrictDTO(District);
            if (District.IsValidated)
                return DistrictDetail_DistrictDTO;
            else
                return BadRequest(DistrictDetail_DistrictDTO);        
        }

        [Route(DistrictDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<DistrictDetail_DistrictDTO>> Delete([FromBody] DistrictDetail_DistrictDTO DistrictDetail_DistrictDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            District District = ConvertDTOToEntity(DistrictDetail_DistrictDTO);

            District = await DistrictService.Delete(District);
            DistrictDetail_DistrictDTO = new DistrictDetail_DistrictDTO(District);
            if (District.IsValidated)
                return DistrictDetail_DistrictDTO;
            else
                return BadRequest(DistrictDetail_DistrictDTO);        
        }

        public District ConvertDTOToEntity(DistrictDetail_DistrictDTO DistrictDetail_DistrictDTO)
        {
            District District = new District();
            
            District.Id = DistrictDetail_DistrictDTO.Id;
            District.Name = DistrictDetail_DistrictDTO.Name;
            District.OrderNumber = DistrictDetail_DistrictDTO.OrderNumber;
            District.ProvinceId = DistrictDetail_DistrictDTO.ProvinceId;
            return District;
        }
        
        
        [Route(DistrictDetailRoute.SingleListProvince), HttpPost]
        public async Task<List<DistrictDetail_ProvinceDTO>> SingleListProvince([FromBody] DistrictDetail_ProvinceFilterDTO DistrictDetail_ProvinceFilterDTO)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            
            ProvinceFilter.Id = new LongFilter{ Equal = DistrictDetail_ProvinceFilterDTO.Id };
            ProvinceFilter.Name = new StringFilter{ StartsWith = DistrictDetail_ProvinceFilterDTO.Name };
            ProvinceFilter.OrderNumber = new LongFilter{ Equal = DistrictDetail_ProvinceFilterDTO.OrderNumber };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<DistrictDetail_ProvinceDTO> DistrictDetail_ProvinceDTOs = Provinces
                .Select(x => new DistrictDetail_ProvinceDTO(x)).ToList();
            return DistrictDetail_ProvinceDTOs;
        }

    }
}

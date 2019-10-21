

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDistrict;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MProvince;


namespace WG.Controllers.district.district_master
{
    public class DistrictMasterRoute : Root
    {
        public const string FE = "/district/district-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListProvince="/single-list-province";
    }

    public class DistrictMasterController : ApiController
    {
        
        
        private IProvinceService ProvinceService;
        private IDistrictService DistrictService;

        public DistrictMasterController(
            
            IProvinceService ProvinceService,
            IDistrictService DistrictService
        )
        {
            
            this.ProvinceService = ProvinceService;
            this.DistrictService = DistrictService;
        }


        [Route(DistrictMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] DistrictMaster_DistrictFilterDTO DistrictMaster_DistrictFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DistrictFilter DistrictFilter = ConvertFilterDTOToFilterEntity(DistrictMaster_DistrictFilterDTO);

            return await DistrictService.Count(DistrictFilter);
        }

        [Route(DistrictMasterRoute.List), HttpPost]
        public async Task<List<DistrictMaster_DistrictDTO>> List([FromBody] DistrictMaster_DistrictFilterDTO DistrictMaster_DistrictFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DistrictFilter DistrictFilter = ConvertFilterDTOToFilterEntity(DistrictMaster_DistrictFilterDTO);

            List<District> Districts = await DistrictService.List(DistrictFilter);

            return Districts.Select(c => new DistrictMaster_DistrictDTO(c)).ToList();
        }

        [Route(DistrictMasterRoute.Get), HttpPost]
        public async Task<DistrictMaster_DistrictDTO> Get([FromBody]DistrictMaster_DistrictDTO DistrictMaster_DistrictDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            District District = await DistrictService.Get(DistrictMaster_DistrictDTO.Id);
            return new DistrictMaster_DistrictDTO(District);
        }


        public DistrictFilter ConvertFilterDTOToFilterEntity(DistrictMaster_DistrictFilterDTO DistrictMaster_DistrictFilterDTO)
        {
            DistrictFilter DistrictFilter = new DistrictFilter();
            
            DistrictFilter.Id = new LongFilter{ Equal = DistrictMaster_DistrictFilterDTO.Id };
            DistrictFilter.Name = new StringFilter{ StartsWith = DistrictMaster_DistrictFilterDTO.Name };
            DistrictFilter.OrderNumber = new LongFilter{ Equal = DistrictMaster_DistrictFilterDTO.OrderNumber };
            DistrictFilter.ProvinceId = new LongFilter{ Equal = DistrictMaster_DistrictFilterDTO.ProvinceId };
            return DistrictFilter;
        }
        
        
        [Route(DistrictMasterRoute.SingleListProvince), HttpPost]
        public async Task<List<DistrictMaster_ProvinceDTO>> SingleListProvince([FromBody] DistrictMaster_ProvinceFilterDTO DistrictMaster_ProvinceFilterDTO)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            
            ProvinceFilter.Id = new LongFilter{ Equal = DistrictMaster_ProvinceFilterDTO.Id };
            ProvinceFilter.Name = new StringFilter{ StartsWith = DistrictMaster_ProvinceFilterDTO.Name };
            ProvinceFilter.OrderNumber = new LongFilter{ Equal = DistrictMaster_ProvinceFilterDTO.OrderNumber };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<DistrictMaster_ProvinceDTO> DistrictMaster_ProvinceDTOs = Provinces
                .Select(x => new DistrictMaster_ProvinceDTO(x)).ToList();
            return DistrictMaster_ProvinceDTOs;
        }

    }
}

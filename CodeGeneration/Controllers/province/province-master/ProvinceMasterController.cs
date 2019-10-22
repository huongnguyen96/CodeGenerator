

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MProvince;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.province.province_master
{
    public class ProvinceMasterRoute : Root
    {
        public const string FE = "/province/province-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class ProvinceMasterController : ApiController
    {
        
        
        private IProvinceService ProvinceService;

        public ProvinceMasterController(
            
            IProvinceService ProvinceService
        )
        {
            
            this.ProvinceService = ProvinceService;
        }


        [Route(ProvinceMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ProvinceMaster_ProvinceFilterDTO ProvinceMaster_ProvinceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProvinceFilter ProvinceFilter = ConvertFilterDTOToFilterEntity(ProvinceMaster_ProvinceFilterDTO);

            return await ProvinceService.Count(ProvinceFilter);
        }

        [Route(ProvinceMasterRoute.List), HttpPost]
        public async Task<List<ProvinceMaster_ProvinceDTO>> List([FromBody] ProvinceMaster_ProvinceFilterDTO ProvinceMaster_ProvinceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProvinceFilter ProvinceFilter = ConvertFilterDTOToFilterEntity(ProvinceMaster_ProvinceFilterDTO);

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);

            return Provinces.Select(c => new ProvinceMaster_ProvinceDTO(c)).ToList();
        }

        [Route(ProvinceMasterRoute.Get), HttpPost]
        public async Task<ProvinceMaster_ProvinceDTO> Get([FromBody]ProvinceMaster_ProvinceDTO ProvinceMaster_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Province Province = await ProvinceService.Get(ProvinceMaster_ProvinceDTO.Id);
            return new ProvinceMaster_ProvinceDTO(Province);
        }


        public ProvinceFilter ConvertFilterDTOToFilterEntity(ProvinceMaster_ProvinceFilterDTO ProvinceMaster_ProvinceFilterDTO)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            
            ProvinceFilter.Id = new LongFilter{ Equal = ProvinceMaster_ProvinceFilterDTO.Id };
            ProvinceFilter.Name = new StringFilter{ StartsWith = ProvinceMaster_ProvinceFilterDTO.Name };
            ProvinceFilter.OrderNumber = new LongFilter{ Equal = ProvinceMaster_ProvinceFilterDTO.OrderNumber };
            return ProvinceFilter;
        }
        
        
    }
}

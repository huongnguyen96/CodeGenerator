

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWard;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MDistrict;


namespace WG.Controllers.ward.ward_master
{
    public class WardMasterRoute : Root
    {
        public const string FE = "/ward/ward-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListDistrict="/single-list-district";
    }

    public class WardMasterController : ApiController
    {
        
        
        private IDistrictService DistrictService;
        private IWardService WardService;

        public WardMasterController(
            
            IDistrictService DistrictService,
            IWardService WardService
        )
        {
            
            this.DistrictService = DistrictService;
            this.WardService = WardService;
        }


        [Route(WardMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] WardMaster_WardFilterDTO WardMaster_WardFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WardFilter WardFilter = ConvertFilterDTOToFilterEntity(WardMaster_WardFilterDTO);

            return await WardService.Count(WardFilter);
        }

        [Route(WardMasterRoute.List), HttpPost]
        public async Task<List<WardMaster_WardDTO>> List([FromBody] WardMaster_WardFilterDTO WardMaster_WardFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WardFilter WardFilter = ConvertFilterDTOToFilterEntity(WardMaster_WardFilterDTO);

            List<Ward> Wards = await WardService.List(WardFilter);

            return Wards.Select(c => new WardMaster_WardDTO(c)).ToList();
        }

        [Route(WardMasterRoute.Get), HttpPost]
        public async Task<WardMaster_WardDTO> Get([FromBody]WardMaster_WardDTO WardMaster_WardDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Ward Ward = await WardService.Get(WardMaster_WardDTO.Id);
            return new WardMaster_WardDTO(Ward);
        }


        public WardFilter ConvertFilterDTOToFilterEntity(WardMaster_WardFilterDTO WardMaster_WardFilterDTO)
        {
            WardFilter WardFilter = new WardFilter();
            WardFilter.Selects = WardSelect.ALL;
            
            WardFilter.Id = new LongFilter{ Equal = WardMaster_WardFilterDTO.Id };
            WardFilter.Name = new StringFilter{ StartsWith = WardMaster_WardFilterDTO.Name };
            WardFilter.OrderNumber = new LongFilter{ Equal = WardMaster_WardFilterDTO.OrderNumber };
            WardFilter.DistrictId = new LongFilter{ Equal = WardMaster_WardFilterDTO.DistrictId };
            return WardFilter;
        }
        
        
        [Route(WardMasterRoute.SingleListDistrict), HttpPost]
        public async Task<List<WardMaster_DistrictDTO>> SingleListDistrict([FromBody] WardMaster_DistrictFilterDTO WardMaster_DistrictFilterDTO)
        {
            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            
            DistrictFilter.Id = new LongFilter{ Equal = WardMaster_DistrictFilterDTO.Id };
            DistrictFilter.Name = new StringFilter{ StartsWith = WardMaster_DistrictFilterDTO.Name };
            DistrictFilter.OrderNumber = new LongFilter{ Equal = WardMaster_DistrictFilterDTO.OrderNumber };
            DistrictFilter.ProvinceId = new LongFilter{ Equal = WardMaster_DistrictFilterDTO.ProvinceId };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<WardMaster_DistrictDTO> WardMaster_DistrictDTOs = Districts
                .Select(x => new WardMaster_DistrictDTO(x)).ToList();
            return WardMaster_DistrictDTOs;
        }

    }
}

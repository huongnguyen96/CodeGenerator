

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWard;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MDistrict;


namespace WG.Controllers.ward.ward_detail
{
    public class WardDetailRoute : Root
    {
        public const string FE = "/ward/ward-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListDistrict="/single-list-district";
    }

    public class WardDetailController : ApiController
    {
        
        
        private IDistrictService DistrictService;
        private IWardService WardService;

        public WardDetailController(
            
            IDistrictService DistrictService,
            IWardService WardService
        )
        {
            
            this.DistrictService = DistrictService;
            this.WardService = WardService;
        }


        [Route(WardDetailRoute.Get), HttpPost]
        public async Task<WardDetail_WardDTO> Get([FromBody]WardDetail_WardDTO WardDetail_WardDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Ward Ward = await WardService.Get(WardDetail_WardDTO.Id);
            return new WardDetail_WardDTO(Ward);
        }


        [Route(WardDetailRoute.Create), HttpPost]
        public async Task<ActionResult<WardDetail_WardDTO>> Create([FromBody] WardDetail_WardDTO WardDetail_WardDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Ward Ward = ConvertDTOToEntity(WardDetail_WardDTO);

            Ward = await WardService.Create(Ward);
            WardDetail_WardDTO = new WardDetail_WardDTO(Ward);
            if (Ward.IsValidated)
                return WardDetail_WardDTO;
            else
                return BadRequest(WardDetail_WardDTO);        
        }

        [Route(WardDetailRoute.Update), HttpPost]
        public async Task<ActionResult<WardDetail_WardDTO>> Update([FromBody] WardDetail_WardDTO WardDetail_WardDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Ward Ward = ConvertDTOToEntity(WardDetail_WardDTO);

            Ward = await WardService.Update(Ward);
            WardDetail_WardDTO = new WardDetail_WardDTO(Ward);
            if (Ward.IsValidated)
                return WardDetail_WardDTO;
            else
                return BadRequest(WardDetail_WardDTO);        
        }

        [Route(WardDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<WardDetail_WardDTO>> Delete([FromBody] WardDetail_WardDTO WardDetail_WardDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Ward Ward = ConvertDTOToEntity(WardDetail_WardDTO);

            Ward = await WardService.Delete(Ward);
            WardDetail_WardDTO = new WardDetail_WardDTO(Ward);
            if (Ward.IsValidated)
                return WardDetail_WardDTO;
            else
                return BadRequest(WardDetail_WardDTO);        
        }

        public Ward ConvertDTOToEntity(WardDetail_WardDTO WardDetail_WardDTO)
        {
            Ward Ward = new Ward();
            
            Ward.Id = WardDetail_WardDTO.Id;
            Ward.Name = WardDetail_WardDTO.Name;
            Ward.OrderNumber = WardDetail_WardDTO.OrderNumber;
            Ward.DistrictId = WardDetail_WardDTO.DistrictId;
            return Ward;
        }
        
        
        [Route(WardDetailRoute.SingleListDistrict), HttpPost]
        public async Task<List<WardDetail_DistrictDTO>> SingleListDistrict([FromBody] WardDetail_DistrictFilterDTO WardDetail_DistrictFilterDTO)
        {
            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            
            DistrictFilter.Id = new LongFilter{ Equal = WardDetail_DistrictFilterDTO.Id };
            DistrictFilter.Name = new StringFilter{ StartsWith = WardDetail_DistrictFilterDTO.Name };
            DistrictFilter.OrderNumber = new LongFilter{ Equal = WardDetail_DistrictFilterDTO.OrderNumber };
            DistrictFilter.ProvinceId = new LongFilter{ Equal = WardDetail_DistrictFilterDTO.ProvinceId };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<WardDetail_DistrictDTO> WardDetail_DistrictDTOs = Districts
                .Select(x => new WardDetail_DistrictDTO(x)).ToList();
            return WardDetail_DistrictDTOs;
        }

    }
}

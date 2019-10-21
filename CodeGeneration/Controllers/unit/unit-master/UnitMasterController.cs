

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MUnit;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MVariation;
using WG.Services.MVariation;
using WG.Services.MVariation;


namespace WG.Controllers.unit.unit_master
{
    public class UnitMasterRoute : Root
    {
        public const string FE = "/unit/unit-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListVariation="/single-list-variation";
    }

    public class UnitMasterController : ApiController
    {
        
        
        private IVariationService VariationService;
        private IUnitService UnitService;

        public UnitMasterController(
            
            IVariationService VariationService,
            IUnitService UnitService
        )
        {
            
            this.VariationService = VariationService;
            this.UnitService = UnitService;
        }


        [Route(UnitMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] UnitMaster_UnitFilterDTO UnitMaster_UnitFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            UnitFilter UnitFilter = ConvertFilterDTOToFilterEntity(UnitMaster_UnitFilterDTO);

            return await UnitService.Count(UnitFilter);
        }

        [Route(UnitMasterRoute.List), HttpPost]
        public async Task<List<UnitMaster_UnitDTO>> List([FromBody] UnitMaster_UnitFilterDTO UnitMaster_UnitFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            UnitFilter UnitFilter = ConvertFilterDTOToFilterEntity(UnitMaster_UnitFilterDTO);

            List<Unit> Units = await UnitService.List(UnitFilter);

            return Units.Select(c => new UnitMaster_UnitDTO(c)).ToList();
        }

        [Route(UnitMasterRoute.Get), HttpPost]
        public async Task<UnitMaster_UnitDTO> Get([FromBody]UnitMaster_UnitDTO UnitMaster_UnitDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Unit Unit = await UnitService.Get(UnitMaster_UnitDTO.Id);
            return new UnitMaster_UnitDTO(Unit);
        }


        public UnitFilter ConvertFilterDTOToFilterEntity(UnitMaster_UnitFilterDTO UnitMaster_UnitFilterDTO)
        {
            UnitFilter UnitFilter = new UnitFilter();
            
            UnitFilter.Id = new LongFilter{ Equal = UnitMaster_UnitFilterDTO.Id };
            UnitFilter.FirstVariationId = new LongFilter{ Equal = UnitMaster_UnitFilterDTO.FirstVariationId };
            UnitFilter.SecondVariationId = new LongFilter{ Equal = UnitMaster_UnitFilterDTO.SecondVariationId };
            UnitFilter.ThirdVariationId = new LongFilter{ Equal = UnitMaster_UnitFilterDTO.ThirdVariationId };
            UnitFilter.SKU = new StringFilter{ StartsWith = UnitMaster_UnitFilterDTO.SKU };
            UnitFilter.Price = new LongFilter{ Equal = UnitMaster_UnitFilterDTO.Price };
            return UnitFilter;
        }
        
        
        [Route(UnitMasterRoute.SingleListVariation), HttpPost]
        public async Task<List<UnitMaster_VariationDTO>> SingleListVariation([FromBody] UnitMaster_VariationFilterDTO UnitMaster_VariationFilterDTO)
        {
            VariationFilter VariationFilter = new VariationFilter();
            VariationFilter.Skip = 0;
            VariationFilter.Take = 20;
            VariationFilter.OrderBy = VariationOrder.Id;
            VariationFilter.OrderType = OrderType.ASC;
            VariationFilter.Selects = VariationSelect.ALL;
            
            VariationFilter.Id = new LongFilter{ Equal = UnitMaster_VariationFilterDTO.Id };
            VariationFilter.Name = new StringFilter{ StartsWith = UnitMaster_VariationFilterDTO.Name };
            VariationFilter.VariationGroupingId = new LongFilter{ Equal = UnitMaster_VariationFilterDTO.VariationGroupingId };

            List<Variation> Variations = await VariationService.List(VariationFilter);
            List<UnitMaster_VariationDTO> UnitMaster_VariationDTOs = Variations
                .Select(x => new UnitMaster_VariationDTO(x)).ToList();
            return UnitMaster_VariationDTOs;
        }

    }
}

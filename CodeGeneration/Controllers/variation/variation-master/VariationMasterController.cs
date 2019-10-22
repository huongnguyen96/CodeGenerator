

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MVariation;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MVariationGrouping;


namespace WG.Controllers.variation.variation_master
{
    public class VariationMasterRoute : Root
    {
        public const string FE = "/variation/variation-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListVariationGrouping= Default + "/single-list-variation-grouping";
    }

    public class VariationMasterController : ApiController
    {
        
        
        private IVariationGroupingService VariationGroupingService;
        private IVariationService VariationService;

        public VariationMasterController(
            
            IVariationGroupingService VariationGroupingService,
            IVariationService VariationService
        )
        {
            
            this.VariationGroupingService = VariationGroupingService;
            this.VariationService = VariationService;
        }


        [Route(VariationMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] VariationMaster_VariationFilterDTO VariationMaster_VariationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationFilter VariationFilter = ConvertFilterDTOToFilterEntity(VariationMaster_VariationFilterDTO);

            return await VariationService.Count(VariationFilter);
        }

        [Route(VariationMasterRoute.List), HttpPost]
        public async Task<List<VariationMaster_VariationDTO>> List([FromBody] VariationMaster_VariationFilterDTO VariationMaster_VariationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationFilter VariationFilter = ConvertFilterDTOToFilterEntity(VariationMaster_VariationFilterDTO);

            List<Variation> Variations = await VariationService.List(VariationFilter);

            return Variations.Select(c => new VariationMaster_VariationDTO(c)).ToList();
        }

        [Route(VariationMasterRoute.Get), HttpPost]
        public async Task<VariationMaster_VariationDTO> Get([FromBody]VariationMaster_VariationDTO VariationMaster_VariationDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Variation Variation = await VariationService.Get(VariationMaster_VariationDTO.Id);
            return new VariationMaster_VariationDTO(Variation);
        }


        public VariationFilter ConvertFilterDTOToFilterEntity(VariationMaster_VariationFilterDTO VariationMaster_VariationFilterDTO)
        {
            VariationFilter VariationFilter = new VariationFilter();
            VariationFilter.Selects = VariationSelect.ALL;
            
            VariationFilter.Id = new LongFilter{ Equal = VariationMaster_VariationFilterDTO.Id };
            VariationFilter.Name = new StringFilter{ StartsWith = VariationMaster_VariationFilterDTO.Name };
            VariationFilter.VariationGroupingId = new LongFilter{ Equal = VariationMaster_VariationFilterDTO.VariationGroupingId };
            return VariationFilter;
        }
        
        
        [Route(VariationMasterRoute.SingleListVariationGrouping), HttpPost]
        public async Task<List<VariationMaster_VariationGroupingDTO>> SingleListVariationGrouping([FromBody] VariationMaster_VariationGroupingFilterDTO VariationMaster_VariationGroupingFilterDTO)
        {
            VariationGroupingFilter VariationGroupingFilter = new VariationGroupingFilter();
            VariationGroupingFilter.Skip = 0;
            VariationGroupingFilter.Take = 20;
            VariationGroupingFilter.OrderBy = VariationGroupingOrder.Id;
            VariationGroupingFilter.OrderType = OrderType.ASC;
            VariationGroupingFilter.Selects = VariationGroupingSelect.ALL;
            
            VariationGroupingFilter.Id = new LongFilter{ Equal = VariationMaster_VariationGroupingFilterDTO.Id };
            VariationGroupingFilter.Name = new StringFilter{ StartsWith = VariationMaster_VariationGroupingFilterDTO.Name };
            VariationGroupingFilter.ItemId = new LongFilter{ Equal = VariationMaster_VariationGroupingFilterDTO.ItemId };

            List<VariationGrouping> VariationGroupings = await VariationGroupingService.List(VariationGroupingFilter);
            List<VariationMaster_VariationGroupingDTO> VariationMaster_VariationGroupingDTOs = VariationGroupings
                .Select(x => new VariationMaster_VariationGroupingDTO(x)).ToList();
            return VariationMaster_VariationGroupingDTOs;
        }

    }
}

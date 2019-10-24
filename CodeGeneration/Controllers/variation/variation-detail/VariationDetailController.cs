

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MVariation;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MVariationGrouping;


namespace WG.Controllers.variation.variation_detail
{
    public class VariationDetailRoute : Root
    {
        public const string FE = "/variation/variation-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListVariationGrouping= Default + "/single-list-variation-grouping";
    }

    public class VariationDetailController : ApiController
    {
        
        
        private IVariationGroupingService VariationGroupingService;
        private IVariationService VariationService;

        public VariationDetailController(
            
            IVariationGroupingService VariationGroupingService,
            IVariationService VariationService
        )
        {
            
            this.VariationGroupingService = VariationGroupingService;
            this.VariationService = VariationService;
        }


        [Route(VariationDetailRoute.Get), HttpPost]
        public async Task<VariationDetail_VariationDTO> Get([FromBody]VariationDetail_VariationDTO VariationDetail_VariationDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Variation Variation = await VariationService.Get(VariationDetail_VariationDTO.Id);
            return new VariationDetail_VariationDTO(Variation);
        }


        [Route(VariationDetailRoute.Create), HttpPost]
        public async Task<ActionResult<VariationDetail_VariationDTO>> Create([FromBody] VariationDetail_VariationDTO VariationDetail_VariationDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Variation Variation = ConvertDTOToEntity(VariationDetail_VariationDTO);

            Variation = await VariationService.Create(Variation);
            VariationDetail_VariationDTO = new VariationDetail_VariationDTO(Variation);
            if (Variation.IsValidated)
                return VariationDetail_VariationDTO;
            else
                return BadRequest(VariationDetail_VariationDTO);        
        }

        [Route(VariationDetailRoute.Update), HttpPost]
        public async Task<ActionResult<VariationDetail_VariationDTO>> Update([FromBody] VariationDetail_VariationDTO VariationDetail_VariationDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Variation Variation = ConvertDTOToEntity(VariationDetail_VariationDTO);

            Variation = await VariationService.Update(Variation);
            VariationDetail_VariationDTO = new VariationDetail_VariationDTO(Variation);
            if (Variation.IsValidated)
                return VariationDetail_VariationDTO;
            else
                return BadRequest(VariationDetail_VariationDTO);        
        }

        [Route(VariationDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<VariationDetail_VariationDTO>> Delete([FromBody] VariationDetail_VariationDTO VariationDetail_VariationDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Variation Variation = ConvertDTOToEntity(VariationDetail_VariationDTO);

            Variation = await VariationService.Delete(Variation);
            VariationDetail_VariationDTO = new VariationDetail_VariationDTO(Variation);
            if (Variation.IsValidated)
                return VariationDetail_VariationDTO;
            else
                return BadRequest(VariationDetail_VariationDTO);        
        }

        public Variation ConvertDTOToEntity(VariationDetail_VariationDTO VariationDetail_VariationDTO)
        {
            Variation Variation = new Variation();
            
            Variation.Id = VariationDetail_VariationDTO.Id;
            Variation.Name = VariationDetail_VariationDTO.Name;
            Variation.VariationGroupingId = VariationDetail_VariationDTO.VariationGroupingId;
            return Variation;
        }
        
        
        [Route(VariationDetailRoute.SingleListVariationGrouping), HttpPost]
        public async Task<List<VariationDetail_VariationGroupingDTO>> SingleListVariationGrouping([FromBody] VariationDetail_VariationGroupingFilterDTO VariationDetail_VariationGroupingFilterDTO)
        {
            VariationGroupingFilter VariationGroupingFilter = new VariationGroupingFilter();
            VariationGroupingFilter.Skip = 0;
            VariationGroupingFilter.Take = 20;
            VariationGroupingFilter.OrderBy = VariationGroupingOrder.Id;
            VariationGroupingFilter.OrderType = OrderType.ASC;
            VariationGroupingFilter.Selects = VariationGroupingSelect.ALL;
            
            VariationGroupingFilter.Id = new LongFilter{ Equal = VariationDetail_VariationGroupingFilterDTO.Id };
            VariationGroupingFilter.Name = new StringFilter{ StartsWith = VariationDetail_VariationGroupingFilterDTO.Name };
            VariationGroupingFilter.ProductId = new LongFilter{ Equal = VariationDetail_VariationGroupingFilterDTO.ProductId };

            List<VariationGrouping> VariationGroupings = await VariationGroupingService.List(VariationGroupingFilter);
            List<VariationDetail_VariationGroupingDTO> VariationDetail_VariationGroupingDTOs = VariationGroupings
                .Select(x => new VariationDetail_VariationGroupingDTO(x)).ToList();
            return VariationDetail_VariationGroupingDTOs;
        }

    }
}

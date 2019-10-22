

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


namespace WG.Controllers.unit.unit_detail
{
    public class UnitDetailRoute : Root
    {
        public const string FE = "/unit/unit-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListVariation= Default + "/single-list-variation";
    }

    public class UnitDetailController : ApiController
    {
        
        
        private IVariationService VariationService;
        private IUnitService UnitService;

        public UnitDetailController(
            
            IVariationService VariationService,
            IUnitService UnitService
        )
        {
            
            this.VariationService = VariationService;
            this.UnitService = UnitService;
        }


        [Route(UnitDetailRoute.Get), HttpPost]
        public async Task<UnitDetail_UnitDTO> Get([FromBody]UnitDetail_UnitDTO UnitDetail_UnitDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Unit Unit = await UnitService.Get(UnitDetail_UnitDTO.Id);
            return new UnitDetail_UnitDTO(Unit);
        }


        [Route(UnitDetailRoute.Create), HttpPost]
        public async Task<ActionResult<UnitDetail_UnitDTO>> Create([FromBody] UnitDetail_UnitDTO UnitDetail_UnitDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Unit Unit = ConvertDTOToEntity(UnitDetail_UnitDTO);

            Unit = await UnitService.Create(Unit);
            UnitDetail_UnitDTO = new UnitDetail_UnitDTO(Unit);
            if (Unit.IsValidated)
                return UnitDetail_UnitDTO;
            else
                return BadRequest(UnitDetail_UnitDTO);        
        }

        [Route(UnitDetailRoute.Update), HttpPost]
        public async Task<ActionResult<UnitDetail_UnitDTO>> Update([FromBody] UnitDetail_UnitDTO UnitDetail_UnitDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Unit Unit = ConvertDTOToEntity(UnitDetail_UnitDTO);

            Unit = await UnitService.Update(Unit);
            UnitDetail_UnitDTO = new UnitDetail_UnitDTO(Unit);
            if (Unit.IsValidated)
                return UnitDetail_UnitDTO;
            else
                return BadRequest(UnitDetail_UnitDTO);        
        }

        [Route(UnitDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<UnitDetail_UnitDTO>> Delete([FromBody] UnitDetail_UnitDTO UnitDetail_UnitDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Unit Unit = ConvertDTOToEntity(UnitDetail_UnitDTO);

            Unit = await UnitService.Delete(Unit);
            UnitDetail_UnitDTO = new UnitDetail_UnitDTO(Unit);
            if (Unit.IsValidated)
                return UnitDetail_UnitDTO;
            else
                return BadRequest(UnitDetail_UnitDTO);        
        }

        public Unit ConvertDTOToEntity(UnitDetail_UnitDTO UnitDetail_UnitDTO)
        {
            Unit Unit = new Unit();
            
            Unit.Id = UnitDetail_UnitDTO.Id;
            Unit.FirstVariationId = UnitDetail_UnitDTO.FirstVariationId;
            Unit.SecondVariationId = UnitDetail_UnitDTO.SecondVariationId;
            Unit.ThirdVariationId = UnitDetail_UnitDTO.ThirdVariationId;
            Unit.SKU = UnitDetail_UnitDTO.SKU;
            Unit.Price = UnitDetail_UnitDTO.Price;
            return Unit;
        }
        
        
        [Route(UnitDetailRoute.SingleListVariation), HttpPost]
        public async Task<List<UnitDetail_VariationDTO>> SingleListVariation([FromBody] UnitDetail_VariationFilterDTO UnitDetail_VariationFilterDTO)
        {
            VariationFilter VariationFilter = new VariationFilter();
            VariationFilter.Skip = 0;
            VariationFilter.Take = 20;
            VariationFilter.OrderBy = VariationOrder.Id;
            VariationFilter.OrderType = OrderType.ASC;
            VariationFilter.Selects = VariationSelect.ALL;
            
            VariationFilter.Id = new LongFilter{ Equal = UnitDetail_VariationFilterDTO.Id };
            VariationFilter.Name = new StringFilter{ StartsWith = UnitDetail_VariationFilterDTO.Name };
            VariationFilter.VariationGroupingId = new LongFilter{ Equal = UnitDetail_VariationFilterDTO.VariationGroupingId };

            List<Variation> Variations = await VariationService.List(VariationFilter);
            List<UnitDetail_VariationDTO> UnitDetail_VariationDTOs = Variations
                .Select(x => new UnitDetail_VariationDTO(x)).ToList();
            return UnitDetail_VariationDTOs;
        }

    }
}

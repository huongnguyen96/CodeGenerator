

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDiscountItem;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MDiscount;
using WG.Services.MUnit;


namespace WG.Controllers.discount_item.discount_item_detail
{
    public class DiscountItemDetailRoute : Root
    {
        public const string FE = "/discount-item/discount-item-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListDiscount="/single-list-discount";
        public const string SingleListUnit="/single-list-unit";
    }

    public class DiscountItemDetailController : ApiController
    {
        
        
        private IDiscountService DiscountService;
        private IUnitService UnitService;
        private IDiscountItemService DiscountItemService;

        public DiscountItemDetailController(
            
            IDiscountService DiscountService,
            IUnitService UnitService,
            IDiscountItemService DiscountItemService
        )
        {
            
            this.DiscountService = DiscountService;
            this.UnitService = UnitService;
            this.DiscountItemService = DiscountItemService;
        }


        [Route(DiscountItemDetailRoute.Get), HttpPost]
        public async Task<DiscountItemDetail_DiscountItemDTO> Get([FromBody]DiscountItemDetail_DiscountItemDTO DiscountItemDetail_DiscountItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountItem DiscountItem = await DiscountItemService.Get(DiscountItemDetail_DiscountItemDTO.Id);
            return new DiscountItemDetail_DiscountItemDTO(DiscountItem);
        }


        [Route(DiscountItemDetailRoute.Create), HttpPost]
        public async Task<ActionResult<DiscountItemDetail_DiscountItemDTO>> Create([FromBody] DiscountItemDetail_DiscountItemDTO DiscountItemDetail_DiscountItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountItem DiscountItem = ConvertDTOToEntity(DiscountItemDetail_DiscountItemDTO);

            DiscountItem = await DiscountItemService.Create(DiscountItem);
            DiscountItemDetail_DiscountItemDTO = new DiscountItemDetail_DiscountItemDTO(DiscountItem);
            if (DiscountItem.IsValidated)
                return DiscountItemDetail_DiscountItemDTO;
            else
                return BadRequest(DiscountItemDetail_DiscountItemDTO);        
        }

        [Route(DiscountItemDetailRoute.Update), HttpPost]
        public async Task<ActionResult<DiscountItemDetail_DiscountItemDTO>> Update([FromBody] DiscountItemDetail_DiscountItemDTO DiscountItemDetail_DiscountItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountItem DiscountItem = ConvertDTOToEntity(DiscountItemDetail_DiscountItemDTO);

            DiscountItem = await DiscountItemService.Update(DiscountItem);
            DiscountItemDetail_DiscountItemDTO = new DiscountItemDetail_DiscountItemDTO(DiscountItem);
            if (DiscountItem.IsValidated)
                return DiscountItemDetail_DiscountItemDTO;
            else
                return BadRequest(DiscountItemDetail_DiscountItemDTO);        
        }

        [Route(DiscountItemDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<DiscountItemDetail_DiscountItemDTO>> Delete([FromBody] DiscountItemDetail_DiscountItemDTO DiscountItemDetail_DiscountItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountItem DiscountItem = ConvertDTOToEntity(DiscountItemDetail_DiscountItemDTO);

            DiscountItem = await DiscountItemService.Delete(DiscountItem);
            DiscountItemDetail_DiscountItemDTO = new DiscountItemDetail_DiscountItemDTO(DiscountItem);
            if (DiscountItem.IsValidated)
                return DiscountItemDetail_DiscountItemDTO;
            else
                return BadRequest(DiscountItemDetail_DiscountItemDTO);        
        }

        public DiscountItem ConvertDTOToEntity(DiscountItemDetail_DiscountItemDTO DiscountItemDetail_DiscountItemDTO)
        {
            DiscountItem DiscountItem = new DiscountItem();
            
            DiscountItem.Id = DiscountItemDetail_DiscountItemDTO.Id;
            DiscountItem.UnitId = DiscountItemDetail_DiscountItemDTO.UnitId;
            DiscountItem.DiscountValue = DiscountItemDetail_DiscountItemDTO.DiscountValue;
            DiscountItem.DiscountId = DiscountItemDetail_DiscountItemDTO.DiscountId;
            return DiscountItem;
        }
        
        
        [Route(DiscountItemDetailRoute.SingleListDiscount), HttpPost]
        public async Task<List<DiscountItemDetail_DiscountDTO>> SingleListDiscount([FromBody] DiscountItemDetail_DiscountFilterDTO DiscountItemDetail_DiscountFilterDTO)
        {
            DiscountFilter DiscountFilter = new DiscountFilter();
            DiscountFilter.Skip = 0;
            DiscountFilter.Take = 20;
            DiscountFilter.OrderBy = DiscountOrder.Id;
            DiscountFilter.OrderType = OrderType.ASC;
            DiscountFilter.Selects = DiscountSelect.ALL;
            
            DiscountFilter.Id = new LongFilter{ Equal = DiscountItemDetail_DiscountFilterDTO.Id };
            DiscountFilter.Name = new StringFilter{ StartsWith = DiscountItemDetail_DiscountFilterDTO.Name };
            DiscountFilter.Start = new DateTimeFilter{ Equal = DiscountItemDetail_DiscountFilterDTO.Start };
            DiscountFilter.End = new DateTimeFilter{ Equal = DiscountItemDetail_DiscountFilterDTO.End };
            DiscountFilter.Type = new StringFilter{ StartsWith = DiscountItemDetail_DiscountFilterDTO.Type };

            List<Discount> Discounts = await DiscountService.List(DiscountFilter);
            List<DiscountItemDetail_DiscountDTO> DiscountItemDetail_DiscountDTOs = Discounts
                .Select(x => new DiscountItemDetail_DiscountDTO(x)).ToList();
            return DiscountItemDetail_DiscountDTOs;
        }

        [Route(DiscountItemDetailRoute.SingleListUnit), HttpPost]
        public async Task<List<DiscountItemDetail_UnitDTO>> SingleListUnit([FromBody] DiscountItemDetail_UnitFilterDTO DiscountItemDetail_UnitFilterDTO)
        {
            UnitFilter UnitFilter = new UnitFilter();
            UnitFilter.Skip = 0;
            UnitFilter.Take = 20;
            UnitFilter.OrderBy = UnitOrder.Id;
            UnitFilter.OrderType = OrderType.ASC;
            UnitFilter.Selects = UnitSelect.ALL;
            
            UnitFilter.Id = new LongFilter{ Equal = DiscountItemDetail_UnitFilterDTO.Id };
            UnitFilter.FirstVariationId = new LongFilter{ Equal = DiscountItemDetail_UnitFilterDTO.FirstVariationId };
            UnitFilter.SecondVariationId = new LongFilter{ Equal = DiscountItemDetail_UnitFilterDTO.SecondVariationId };
            UnitFilter.ThirdVariationId = new LongFilter{ Equal = DiscountItemDetail_UnitFilterDTO.ThirdVariationId };
            UnitFilter.SKU = new StringFilter{ StartsWith = DiscountItemDetail_UnitFilterDTO.SKU };
            UnitFilter.Price = new LongFilter{ Equal = DiscountItemDetail_UnitFilterDTO.Price };

            List<Unit> Units = await UnitService.List(UnitFilter);
            List<DiscountItemDetail_UnitDTO> DiscountItemDetail_UnitDTOs = Units
                .Select(x => new DiscountItemDetail_UnitDTO(x)).ToList();
            return DiscountItemDetail_UnitDTOs;
        }

    }
}

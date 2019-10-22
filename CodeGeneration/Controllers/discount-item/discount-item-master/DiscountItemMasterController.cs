

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


namespace WG.Controllers.discount_item.discount_item_master
{
    public class DiscountItemMasterRoute : Root
    {
        public const string FE = "/discount-item/discount-item-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListDiscount= Default + "/single-list-discount";
        public const string SingleListUnit= Default + "/single-list-unit";
    }

    public class DiscountItemMasterController : ApiController
    {
        
        
        private IDiscountService DiscountService;
        private IUnitService UnitService;
        private IDiscountItemService DiscountItemService;

        public DiscountItemMasterController(
            
            IDiscountService DiscountService,
            IUnitService UnitService,
            IDiscountItemService DiscountItemService
        )
        {
            
            this.DiscountService = DiscountService;
            this.UnitService = UnitService;
            this.DiscountItemService = DiscountItemService;
        }


        [Route(DiscountItemMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] DiscountItemMaster_DiscountItemFilterDTO DiscountItemMaster_DiscountItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountItemFilter DiscountItemFilter = ConvertFilterDTOToFilterEntity(DiscountItemMaster_DiscountItemFilterDTO);

            return await DiscountItemService.Count(DiscountItemFilter);
        }

        [Route(DiscountItemMasterRoute.List), HttpPost]
        public async Task<List<DiscountItemMaster_DiscountItemDTO>> List([FromBody] DiscountItemMaster_DiscountItemFilterDTO DiscountItemMaster_DiscountItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountItemFilter DiscountItemFilter = ConvertFilterDTOToFilterEntity(DiscountItemMaster_DiscountItemFilterDTO);

            List<DiscountItem> DiscountItems = await DiscountItemService.List(DiscountItemFilter);

            return DiscountItems.Select(c => new DiscountItemMaster_DiscountItemDTO(c)).ToList();
        }

        [Route(DiscountItemMasterRoute.Get), HttpPost]
        public async Task<DiscountItemMaster_DiscountItemDTO> Get([FromBody]DiscountItemMaster_DiscountItemDTO DiscountItemMaster_DiscountItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountItem DiscountItem = await DiscountItemService.Get(DiscountItemMaster_DiscountItemDTO.Id);
            return new DiscountItemMaster_DiscountItemDTO(DiscountItem);
        }


        public DiscountItemFilter ConvertFilterDTOToFilterEntity(DiscountItemMaster_DiscountItemFilterDTO DiscountItemMaster_DiscountItemFilterDTO)
        {
            DiscountItemFilter DiscountItemFilter = new DiscountItemFilter();
            DiscountItemFilter.Selects = DiscountItemSelect.ALL;
            
            DiscountItemFilter.Id = new LongFilter{ Equal = DiscountItemMaster_DiscountItemFilterDTO.Id };
            DiscountItemFilter.UnitId = new LongFilter{ Equal = DiscountItemMaster_DiscountItemFilterDTO.UnitId };
            DiscountItemFilter.DiscountValue = new LongFilter{ Equal = DiscountItemMaster_DiscountItemFilterDTO.DiscountValue };
            DiscountItemFilter.DiscountId = new LongFilter{ Equal = DiscountItemMaster_DiscountItemFilterDTO.DiscountId };
            return DiscountItemFilter;
        }
        
        
        [Route(DiscountItemMasterRoute.SingleListDiscount), HttpPost]
        public async Task<List<DiscountItemMaster_DiscountDTO>> SingleListDiscount([FromBody] DiscountItemMaster_DiscountFilterDTO DiscountItemMaster_DiscountFilterDTO)
        {
            DiscountFilter DiscountFilter = new DiscountFilter();
            DiscountFilter.Skip = 0;
            DiscountFilter.Take = 20;
            DiscountFilter.OrderBy = DiscountOrder.Id;
            DiscountFilter.OrderType = OrderType.ASC;
            DiscountFilter.Selects = DiscountSelect.ALL;
            
            DiscountFilter.Id = new LongFilter{ Equal = DiscountItemMaster_DiscountFilterDTO.Id };
            DiscountFilter.Name = new StringFilter{ StartsWith = DiscountItemMaster_DiscountFilterDTO.Name };
            DiscountFilter.Start = new DateTimeFilter{ Equal = DiscountItemMaster_DiscountFilterDTO.Start };
            DiscountFilter.End = new DateTimeFilter{ Equal = DiscountItemMaster_DiscountFilterDTO.End };
            DiscountFilter.Type = new StringFilter{ StartsWith = DiscountItemMaster_DiscountFilterDTO.Type };

            List<Discount> Discounts = await DiscountService.List(DiscountFilter);
            List<DiscountItemMaster_DiscountDTO> DiscountItemMaster_DiscountDTOs = Discounts
                .Select(x => new DiscountItemMaster_DiscountDTO(x)).ToList();
            return DiscountItemMaster_DiscountDTOs;
        }

        [Route(DiscountItemMasterRoute.SingleListUnit), HttpPost]
        public async Task<List<DiscountItemMaster_UnitDTO>> SingleListUnit([FromBody] DiscountItemMaster_UnitFilterDTO DiscountItemMaster_UnitFilterDTO)
        {
            UnitFilter UnitFilter = new UnitFilter();
            UnitFilter.Skip = 0;
            UnitFilter.Take = 20;
            UnitFilter.OrderBy = UnitOrder.Id;
            UnitFilter.OrderType = OrderType.ASC;
            UnitFilter.Selects = UnitSelect.ALL;
            
            UnitFilter.Id = new LongFilter{ Equal = DiscountItemMaster_UnitFilterDTO.Id };
            UnitFilter.FirstVariationId = new LongFilter{ Equal = DiscountItemMaster_UnitFilterDTO.FirstVariationId };
            UnitFilter.SecondVariationId = new LongFilter{ Equal = DiscountItemMaster_UnitFilterDTO.SecondVariationId };
            UnitFilter.ThirdVariationId = new LongFilter{ Equal = DiscountItemMaster_UnitFilterDTO.ThirdVariationId };
            UnitFilter.SKU = new StringFilter{ StartsWith = DiscountItemMaster_UnitFilterDTO.SKU };
            UnitFilter.Price = new LongFilter{ Equal = DiscountItemMaster_UnitFilterDTO.Price };

            List<Unit> Units = await UnitService.List(UnitFilter);
            List<DiscountItemMaster_UnitDTO> DiscountItemMaster_UnitDTOs = Units
                .Select(x => new DiscountItemMaster_UnitDTO(x)).ToList();
            return DiscountItemMaster_UnitDTOs;
        }

    }
}

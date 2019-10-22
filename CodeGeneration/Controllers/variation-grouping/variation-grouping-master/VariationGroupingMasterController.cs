

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MVariationGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MItem;


namespace WG.Controllers.variation_grouping.variation_grouping_master
{
    public class VariationGroupingMasterRoute : Root
    {
        public const string FE = "/variation-grouping/variation-grouping-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListItem= Default + "/single-list-item";
    }

    public class VariationGroupingMasterController : ApiController
    {
        
        
        private IItemService ItemService;
        private IVariationGroupingService VariationGroupingService;

        public VariationGroupingMasterController(
            
            IItemService ItemService,
            IVariationGroupingService VariationGroupingService
        )
        {
            
            this.ItemService = ItemService;
            this.VariationGroupingService = VariationGroupingService;
        }


        [Route(VariationGroupingMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] VariationGroupingMaster_VariationGroupingFilterDTO VariationGroupingMaster_VariationGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGroupingFilter VariationGroupingFilter = ConvertFilterDTOToFilterEntity(VariationGroupingMaster_VariationGroupingFilterDTO);

            return await VariationGroupingService.Count(VariationGroupingFilter);
        }

        [Route(VariationGroupingMasterRoute.List), HttpPost]
        public async Task<List<VariationGroupingMaster_VariationGroupingDTO>> List([FromBody] VariationGroupingMaster_VariationGroupingFilterDTO VariationGroupingMaster_VariationGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGroupingFilter VariationGroupingFilter = ConvertFilterDTOToFilterEntity(VariationGroupingMaster_VariationGroupingFilterDTO);

            List<VariationGrouping> VariationGroupings = await VariationGroupingService.List(VariationGroupingFilter);

            return VariationGroupings.Select(c => new VariationGroupingMaster_VariationGroupingDTO(c)).ToList();
        }

        [Route(VariationGroupingMasterRoute.Get), HttpPost]
        public async Task<VariationGroupingMaster_VariationGroupingDTO> Get([FromBody]VariationGroupingMaster_VariationGroupingDTO VariationGroupingMaster_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = await VariationGroupingService.Get(VariationGroupingMaster_VariationGroupingDTO.Id);
            return new VariationGroupingMaster_VariationGroupingDTO(VariationGrouping);
        }


        public VariationGroupingFilter ConvertFilterDTOToFilterEntity(VariationGroupingMaster_VariationGroupingFilterDTO VariationGroupingMaster_VariationGroupingFilterDTO)
        {
            VariationGroupingFilter VariationGroupingFilter = new VariationGroupingFilter();
            VariationGroupingFilter.Selects = VariationGroupingSelect.ALL;
            
            VariationGroupingFilter.Id = new LongFilter{ Equal = VariationGroupingMaster_VariationGroupingFilterDTO.Id };
            VariationGroupingFilter.Name = new StringFilter{ StartsWith = VariationGroupingMaster_VariationGroupingFilterDTO.Name };
            VariationGroupingFilter.ItemId = new LongFilter{ Equal = VariationGroupingMaster_VariationGroupingFilterDTO.ItemId };
            return VariationGroupingFilter;
        }
        
        
        [Route(VariationGroupingMasterRoute.SingleListItem), HttpPost]
        public async Task<List<VariationGroupingMaster_ItemDTO>> SingleListItem([FromBody] VariationGroupingMaster_ItemFilterDTO VariationGroupingMaster_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = VariationGroupingMaster_ItemFilterDTO.Id };
            ItemFilter.Code = new StringFilter{ StartsWith = VariationGroupingMaster_ItemFilterDTO.Code };
            ItemFilter.Name = new StringFilter{ StartsWith = VariationGroupingMaster_ItemFilterDTO.Name };
            ItemFilter.SKU = new StringFilter{ StartsWith = VariationGroupingMaster_ItemFilterDTO.SKU };
            ItemFilter.Description = new StringFilter{ StartsWith = VariationGroupingMaster_ItemFilterDTO.Description };
            ItemFilter.TypeId = new LongFilter{ Equal = VariationGroupingMaster_ItemFilterDTO.TypeId };
            ItemFilter.StatusId = new LongFilter{ Equal = VariationGroupingMaster_ItemFilterDTO.StatusId };
            ItemFilter.PartnerId = new LongFilter{ Equal = VariationGroupingMaster_ItemFilterDTO.PartnerId };
            ItemFilter.CategoryId = new LongFilter{ Equal = VariationGroupingMaster_ItemFilterDTO.CategoryId };
            ItemFilter.BrandId = new LongFilter{ Equal = VariationGroupingMaster_ItemFilterDTO.BrandId };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<VariationGroupingMaster_ItemDTO> VariationGroupingMaster_ItemDTOs = Items
                .Select(x => new VariationGroupingMaster_ItemDTO(x)).ToList();
            return VariationGroupingMaster_ItemDTOs;
        }

    }
}

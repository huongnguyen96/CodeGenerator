

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MVariationGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MItem;


namespace WG.Controllers.variation_grouping.variation_grouping_detail
{
    public class VariationGroupingDetailRoute : Root
    {
        public const string FE = "/variation-grouping/variation-grouping-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListItem="/single-list-item";
    }

    public class VariationGroupingDetailController : ApiController
    {
        
        
        private IItemService ItemService;
        private IVariationGroupingService VariationGroupingService;

        public VariationGroupingDetailController(
            
            IItemService ItemService,
            IVariationGroupingService VariationGroupingService
        )
        {
            
            this.ItemService = ItemService;
            this.VariationGroupingService = VariationGroupingService;
        }


        [Route(VariationGroupingDetailRoute.Get), HttpPost]
        public async Task<VariationGroupingDetail_VariationGroupingDTO> Get([FromBody]VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = await VariationGroupingService.Get(VariationGroupingDetail_VariationGroupingDTO.Id);
            return new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
        }


        [Route(VariationGroupingDetailRoute.Create), HttpPost]
        public async Task<ActionResult<VariationGroupingDetail_VariationGroupingDTO>> Create([FromBody] VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO);

            VariationGrouping = await VariationGroupingService.Create(VariationGrouping);
            VariationGroupingDetail_VariationGroupingDTO = new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
            if (VariationGrouping.IsValidated)
                return VariationGroupingDetail_VariationGroupingDTO;
            else
                return BadRequest(VariationGroupingDetail_VariationGroupingDTO);        
        }

        [Route(VariationGroupingDetailRoute.Update), HttpPost]
        public async Task<ActionResult<VariationGroupingDetail_VariationGroupingDTO>> Update([FromBody] VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO);

            VariationGrouping = await VariationGroupingService.Update(VariationGrouping);
            VariationGroupingDetail_VariationGroupingDTO = new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
            if (VariationGrouping.IsValidated)
                return VariationGroupingDetail_VariationGroupingDTO;
            else
                return BadRequest(VariationGroupingDetail_VariationGroupingDTO);        
        }

        [Route(VariationGroupingDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<VariationGroupingDetail_VariationGroupingDTO>> Delete([FromBody] VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO);

            VariationGrouping = await VariationGroupingService.Delete(VariationGrouping);
            VariationGroupingDetail_VariationGroupingDTO = new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
            if (VariationGrouping.IsValidated)
                return VariationGroupingDetail_VariationGroupingDTO;
            else
                return BadRequest(VariationGroupingDetail_VariationGroupingDTO);        
        }

        public VariationGrouping ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            VariationGrouping VariationGrouping = new VariationGrouping();
            
            VariationGrouping.Id = VariationGroupingDetail_VariationGroupingDTO.Id;
            VariationGrouping.Name = VariationGroupingDetail_VariationGroupingDTO.Name;
            VariationGrouping.ItemId = VariationGroupingDetail_VariationGroupingDTO.ItemId;
            return VariationGrouping;
        }
        
        
        [Route(VariationGroupingDetailRoute.SingleListItem), HttpPost]
        public async Task<List<VariationGroupingDetail_ItemDTO>> SingleListItem([FromBody] VariationGroupingDetail_ItemFilterDTO VariationGroupingDetail_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = VariationGroupingDetail_ItemFilterDTO.Id };
            ItemFilter.Code = new StringFilter{ StartsWith = VariationGroupingDetail_ItemFilterDTO.Code };
            ItemFilter.Name = new StringFilter{ StartsWith = VariationGroupingDetail_ItemFilterDTO.Name };
            ItemFilter.SKU = new StringFilter{ StartsWith = VariationGroupingDetail_ItemFilterDTO.SKU };
            ItemFilter.Description = new StringFilter{ StartsWith = VariationGroupingDetail_ItemFilterDTO.Description };
            ItemFilter.TypeId = new LongFilter{ Equal = VariationGroupingDetail_ItemFilterDTO.TypeId };
            ItemFilter.StatusId = new LongFilter{ Equal = VariationGroupingDetail_ItemFilterDTO.StatusId };
            ItemFilter.PartnerId = new LongFilter{ Equal = VariationGroupingDetail_ItemFilterDTO.PartnerId };
            ItemFilter.CategoryId = new LongFilter{ Equal = VariationGroupingDetail_ItemFilterDTO.CategoryId };
            ItemFilter.BrandId = new LongFilter{ Equal = VariationGroupingDetail_ItemFilterDTO.BrandId };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<VariationGroupingDetail_ItemDTO> VariationGroupingDetail_ItemDTOs = Items
                .Select(x => new VariationGroupingDetail_ItemDTO(x)).ToList();
            return VariationGroupingDetail_ItemDTOs;
        }

    }
}

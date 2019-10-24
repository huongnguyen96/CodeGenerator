

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MDiscountContent;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MDiscount;
using WG.Services.MItem;


namespace WG.Controllers.discount_content.discount_content_master
{
    public class DiscountContentMasterRoute : Root
    {
        public const string FE = "/discount-content/discount-content-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListDiscount= Default + "/single-list-discount";
        public const string SingleListItem= Default + "/single-list-item";
    }

    public class DiscountContentMasterController : ApiController
    {
        
        
        private IDiscountService DiscountService;
        private IItemService ItemService;
        private IDiscountContentService DiscountContentService;

        public DiscountContentMasterController(
            
            IDiscountService DiscountService,
            IItemService ItemService,
            IDiscountContentService DiscountContentService
        )
        {
            
            this.DiscountService = DiscountService;
            this.ItemService = ItemService;
            this.DiscountContentService = DiscountContentService;
        }


        [Route(DiscountContentMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] DiscountContentMaster_DiscountContentFilterDTO DiscountContentMaster_DiscountContentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountContentFilter DiscountContentFilter = ConvertFilterDTOToFilterEntity(DiscountContentMaster_DiscountContentFilterDTO);

            return await DiscountContentService.Count(DiscountContentFilter);
        }

        [Route(DiscountContentMasterRoute.List), HttpPost]
        public async Task<List<DiscountContentMaster_DiscountContentDTO>> List([FromBody] DiscountContentMaster_DiscountContentFilterDTO DiscountContentMaster_DiscountContentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountContentFilter DiscountContentFilter = ConvertFilterDTOToFilterEntity(DiscountContentMaster_DiscountContentFilterDTO);

            List<DiscountContent> DiscountContents = await DiscountContentService.List(DiscountContentFilter);

            return DiscountContents.Select(c => new DiscountContentMaster_DiscountContentDTO(c)).ToList();
        }

        [Route(DiscountContentMasterRoute.Get), HttpPost]
        public async Task<DiscountContentMaster_DiscountContentDTO> Get([FromBody]DiscountContentMaster_DiscountContentDTO DiscountContentMaster_DiscountContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountContent DiscountContent = await DiscountContentService.Get(DiscountContentMaster_DiscountContentDTO.Id);
            return new DiscountContentMaster_DiscountContentDTO(DiscountContent);
        }


        public DiscountContentFilter ConvertFilterDTOToFilterEntity(DiscountContentMaster_DiscountContentFilterDTO DiscountContentMaster_DiscountContentFilterDTO)
        {
            DiscountContentFilter DiscountContentFilter = new DiscountContentFilter();
            DiscountContentFilter.Selects = DiscountContentSelect.ALL;
            
            DiscountContentFilter.Id = new LongFilter{ Equal = DiscountContentMaster_DiscountContentFilterDTO.Id };
            DiscountContentFilter.ItemId = new LongFilter{ Equal = DiscountContentMaster_DiscountContentFilterDTO.ItemId };
            DiscountContentFilter.DiscountValue = new LongFilter{ Equal = DiscountContentMaster_DiscountContentFilterDTO.DiscountValue };
            DiscountContentFilter.DiscountId = new LongFilter{ Equal = DiscountContentMaster_DiscountContentFilterDTO.DiscountId };
            return DiscountContentFilter;
        }
        
        
        [Route(DiscountContentMasterRoute.SingleListDiscount), HttpPost]
        public async Task<List<DiscountContentMaster_DiscountDTO>> SingleListDiscount([FromBody] DiscountContentMaster_DiscountFilterDTO DiscountContentMaster_DiscountFilterDTO)
        {
            DiscountFilter DiscountFilter = new DiscountFilter();
            DiscountFilter.Skip = 0;
            DiscountFilter.Take = 20;
            DiscountFilter.OrderBy = DiscountOrder.Id;
            DiscountFilter.OrderType = OrderType.ASC;
            DiscountFilter.Selects = DiscountSelect.ALL;
            
            DiscountFilter.Id = new LongFilter{ Equal = DiscountContentMaster_DiscountFilterDTO.Id };
            DiscountFilter.Name = new StringFilter{ StartsWith = DiscountContentMaster_DiscountFilterDTO.Name };
            DiscountFilter.Start = new DateTimeFilter{ Equal = DiscountContentMaster_DiscountFilterDTO.Start };
            DiscountFilter.End = new DateTimeFilter{ Equal = DiscountContentMaster_DiscountFilterDTO.End };
            DiscountFilter.Type = new StringFilter{ StartsWith = DiscountContentMaster_DiscountFilterDTO.Type };

            List<Discount> Discounts = await DiscountService.List(DiscountFilter);
            List<DiscountContentMaster_DiscountDTO> DiscountContentMaster_DiscountDTOs = Discounts
                .Select(x => new DiscountContentMaster_DiscountDTO(x)).ToList();
            return DiscountContentMaster_DiscountDTOs;
        }

        [Route(DiscountContentMasterRoute.SingleListItem), HttpPost]
        public async Task<List<DiscountContentMaster_ItemDTO>> SingleListItem([FromBody] DiscountContentMaster_ItemFilterDTO DiscountContentMaster_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = DiscountContentMaster_ItemFilterDTO.Id };
            ItemFilter.ProductId = new LongFilter{ Equal = DiscountContentMaster_ItemFilterDTO.ProductId };
            ItemFilter.FirstVariationId = new LongFilter{ Equal = DiscountContentMaster_ItemFilterDTO.FirstVariationId };
            ItemFilter.SecondVariationId = new LongFilter{ Equal = DiscountContentMaster_ItemFilterDTO.SecondVariationId };
            ItemFilter.SKU = new StringFilter{ StartsWith = DiscountContentMaster_ItemFilterDTO.SKU };
            ItemFilter.Price = new LongFilter{ Equal = DiscountContentMaster_ItemFilterDTO.Price };
            ItemFilter.MinPrice = new LongFilter{ Equal = DiscountContentMaster_ItemFilterDTO.MinPrice };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<DiscountContentMaster_ItemDTO> DiscountContentMaster_ItemDTOs = Items
                .Select(x => new DiscountContentMaster_ItemDTO(x)).ToList();
            return DiscountContentMaster_ItemDTOs;
        }

    }
}

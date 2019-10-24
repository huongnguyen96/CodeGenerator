

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


namespace WG.Controllers.discount_content.discount_content_detail
{
    public class DiscountContentDetailRoute : Root
    {
        public const string FE = "/discount-content/discount-content-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListDiscount= Default + "/single-list-discount";
        public const string SingleListItem= Default + "/single-list-item";
    }

    public class DiscountContentDetailController : ApiController
    {
        
        
        private IDiscountService DiscountService;
        private IItemService ItemService;
        private IDiscountContentService DiscountContentService;

        public DiscountContentDetailController(
            
            IDiscountService DiscountService,
            IItemService ItemService,
            IDiscountContentService DiscountContentService
        )
        {
            
            this.DiscountService = DiscountService;
            this.ItemService = ItemService;
            this.DiscountContentService = DiscountContentService;
        }


        [Route(DiscountContentDetailRoute.Get), HttpPost]
        public async Task<DiscountContentDetail_DiscountContentDTO> Get([FromBody]DiscountContentDetail_DiscountContentDTO DiscountContentDetail_DiscountContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountContent DiscountContent = await DiscountContentService.Get(DiscountContentDetail_DiscountContentDTO.Id);
            return new DiscountContentDetail_DiscountContentDTO(DiscountContent);
        }


        [Route(DiscountContentDetailRoute.Create), HttpPost]
        public async Task<ActionResult<DiscountContentDetail_DiscountContentDTO>> Create([FromBody] DiscountContentDetail_DiscountContentDTO DiscountContentDetail_DiscountContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountContent DiscountContent = ConvertDTOToEntity(DiscountContentDetail_DiscountContentDTO);

            DiscountContent = await DiscountContentService.Create(DiscountContent);
            DiscountContentDetail_DiscountContentDTO = new DiscountContentDetail_DiscountContentDTO(DiscountContent);
            if (DiscountContent.IsValidated)
                return DiscountContentDetail_DiscountContentDTO;
            else
                return BadRequest(DiscountContentDetail_DiscountContentDTO);        
        }

        [Route(DiscountContentDetailRoute.Update), HttpPost]
        public async Task<ActionResult<DiscountContentDetail_DiscountContentDTO>> Update([FromBody] DiscountContentDetail_DiscountContentDTO DiscountContentDetail_DiscountContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountContent DiscountContent = ConvertDTOToEntity(DiscountContentDetail_DiscountContentDTO);

            DiscountContent = await DiscountContentService.Update(DiscountContent);
            DiscountContentDetail_DiscountContentDTO = new DiscountContentDetail_DiscountContentDTO(DiscountContent);
            if (DiscountContent.IsValidated)
                return DiscountContentDetail_DiscountContentDTO;
            else
                return BadRequest(DiscountContentDetail_DiscountContentDTO);        
        }

        [Route(DiscountContentDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<DiscountContentDetail_DiscountContentDTO>> Delete([FromBody] DiscountContentDetail_DiscountContentDTO DiscountContentDetail_DiscountContentDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            DiscountContent DiscountContent = ConvertDTOToEntity(DiscountContentDetail_DiscountContentDTO);

            DiscountContent = await DiscountContentService.Delete(DiscountContent);
            DiscountContentDetail_DiscountContentDTO = new DiscountContentDetail_DiscountContentDTO(DiscountContent);
            if (DiscountContent.IsValidated)
                return DiscountContentDetail_DiscountContentDTO;
            else
                return BadRequest(DiscountContentDetail_DiscountContentDTO);        
        }

        public DiscountContent ConvertDTOToEntity(DiscountContentDetail_DiscountContentDTO DiscountContentDetail_DiscountContentDTO)
        {
            DiscountContent DiscountContent = new DiscountContent();
            
            DiscountContent.Id = DiscountContentDetail_DiscountContentDTO.Id;
            DiscountContent.ItemId = DiscountContentDetail_DiscountContentDTO.ItemId;
            DiscountContent.DiscountValue = DiscountContentDetail_DiscountContentDTO.DiscountValue;
            DiscountContent.DiscountId = DiscountContentDetail_DiscountContentDTO.DiscountId;
            return DiscountContent;
        }
        
        
        [Route(DiscountContentDetailRoute.SingleListDiscount), HttpPost]
        public async Task<List<DiscountContentDetail_DiscountDTO>> SingleListDiscount([FromBody] DiscountContentDetail_DiscountFilterDTO DiscountContentDetail_DiscountFilterDTO)
        {
            DiscountFilter DiscountFilter = new DiscountFilter();
            DiscountFilter.Skip = 0;
            DiscountFilter.Take = 20;
            DiscountFilter.OrderBy = DiscountOrder.Id;
            DiscountFilter.OrderType = OrderType.ASC;
            DiscountFilter.Selects = DiscountSelect.ALL;
            
            DiscountFilter.Id = new LongFilter{ Equal = DiscountContentDetail_DiscountFilterDTO.Id };
            DiscountFilter.Name = new StringFilter{ StartsWith = DiscountContentDetail_DiscountFilterDTO.Name };
            DiscountFilter.Start = new DateTimeFilter{ Equal = DiscountContentDetail_DiscountFilterDTO.Start };
            DiscountFilter.End = new DateTimeFilter{ Equal = DiscountContentDetail_DiscountFilterDTO.End };
            DiscountFilter.Type = new StringFilter{ StartsWith = DiscountContentDetail_DiscountFilterDTO.Type };

            List<Discount> Discounts = await DiscountService.List(DiscountFilter);
            List<DiscountContentDetail_DiscountDTO> DiscountContentDetail_DiscountDTOs = Discounts
                .Select(x => new DiscountContentDetail_DiscountDTO(x)).ToList();
            return DiscountContentDetail_DiscountDTOs;
        }

        [Route(DiscountContentDetailRoute.SingleListItem), HttpPost]
        public async Task<List<DiscountContentDetail_ItemDTO>> SingleListItem([FromBody] DiscountContentDetail_ItemFilterDTO DiscountContentDetail_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = DiscountContentDetail_ItemFilterDTO.Id };
            ItemFilter.ProductId = new LongFilter{ Equal = DiscountContentDetail_ItemFilterDTO.ProductId };
            ItemFilter.FirstVariationId = new LongFilter{ Equal = DiscountContentDetail_ItemFilterDTO.FirstVariationId };
            ItemFilter.SecondVariationId = new LongFilter{ Equal = DiscountContentDetail_ItemFilterDTO.SecondVariationId };
            ItemFilter.SKU = new StringFilter{ StartsWith = DiscountContentDetail_ItemFilterDTO.SKU };
            ItemFilter.Price = new LongFilter{ Equal = DiscountContentDetail_ItemFilterDTO.Price };
            ItemFilter.MinPrice = new LongFilter{ Equal = DiscountContentDetail_ItemFilterDTO.MinPrice };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<DiscountContentDetail_ItemDTO> DiscountContentDetail_ItemDTOs = Items
                .Select(x => new DiscountContentDetail_ItemDTO(x)).ToList();
            return DiscountContentDetail_ItemDTOs;
        }

    }
}

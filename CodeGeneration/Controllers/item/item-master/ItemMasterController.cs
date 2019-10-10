
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Services.MItem;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;

namespace WeGift.Controllers.item.item_master
{
    public class ItemMasterRoute : Root
    {
        public const string FE = "item/item-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
    }

    public class ItemMasterController : ApiController
    {
        private IItemService ItemService;

        public ItemMasterController(
            IItemService ItemService
        )
        {
            this.ItemService = ItemService;
        }

        [Route(ItemMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ItemMaster_ItemFilterDTO ItemMaster_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemFilter ItemFilter = ConvertFilterDTOtoFilterEntity(ItemMaster_ItemFilterDTO);

            return await ItemService.Count(ItemFilter);
        }

        [Route(ItemMasterRoute.List), HttpPost]
        public async Task<List<ItemMaster_ItemDTO>> List([FromBody] ItemMaster_ItemFilterDTO ItemMaster_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemFilter ItemFilter = ConvertFilterDTOtoFilterEntity(ItemMaster_ItemFilterDTO);

            List<Item> Items = await ItemService.List(ItemFilter);

            return Items.Select(c => new ItemMaster_ItemDTO(c)).ToList();
        }

        [Route(ItemMasterRoute.Get), HttpPost]
        public async Task<ItemMaster_ItemDTO> Get([FromBody]ItemMaster_ItemDTO ItemMaster_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = await ItemService.Get(ItemMaster_ItemDTO.Id);
            return new ItemMaster_ItemDTO(Item);
        }


        public ItemFilter ConvertFilterDTOtoFilterEntity(ItemMaster_ItemFilterDTO ItemMaster_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            
            ItemFilter.Id = ItemMaster_ItemFilterDTO.Id;
            ItemFilter.Code = ItemMaster_ItemFilterDTO.Code;
            ItemFilter.Name = ItemMaster_ItemFilterDTO.Name;
            return ItemFilter;
        }
    }
}

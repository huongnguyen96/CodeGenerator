
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeGift.Services.MItemService;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;

namespace WeGift.Controllers.item.itemList
{
    public class ItemListRoute : Root
    {
        public const string FE = "item/item-list";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
    }

    public class ItemListController : ApiController
    {
        private IItemService ItemService;

        public ItemListController(
            IItemService ItemService
        )
        {
            this.ItemService = ItemService;
        }

        [Route(ItemListRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ItemList_ItemFilterDTO ItemList_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemFilter ItemFilter = ConvertFilterDTOtoFilterBO(ItemList_ItemFilterDTO);

            return await ItemService.Count(ItemFilter);
        }

        [Route(ItemListRoute.List), HttpPost]
        public async Task<List<ItemList_ItemDTO>> List([FromBody] ItemList_ItemFilterDTO ItemList_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemFilter ItemFilter = ConvertFilterDTOtoFilterEntity(ItemList_ItemFilterDTO);

            List<Item> Items = await ItemService.List(CustomerFilter);

            return Items.Select(c => new ItemList_ItemDTO(c)).ToList();
        }

        [Route(ItemListRoute.Get), HttpPost]
        public async Task<ItemList_ItemDTO> Get([FromBody]ItemList_ItemDTO ItemList_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = await ItemService.Get(ItemList_ItemDTO.Id);
            return new ItemList_ItemDTO(Customer);
        }


        public ItemFilter ConvertFilterDTOtoFilterEntity(ItemList_ItemFilterDTO ItemFilter_ItemDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            return ItemFilter;
        }
    }
}

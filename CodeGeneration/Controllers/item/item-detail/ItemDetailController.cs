

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItem;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.item.item_detail
{
    public class ItemDetailRoute : Root
    {
        public const string FE = "/item/item-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ItemDetailController : ApiController
    {
        
        
        private IItemService ItemService;

        public ItemDetailController(
            
            IItemService ItemService
        )
        {
            
            this.ItemService = ItemService;
        }


        [Route(ItemDetailRoute.Get), HttpPost]
        public async Task<ItemDetail_ItemDTO> Get([FromBody]ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = await ItemService.Get(ItemDetail_ItemDTO.Id);
            return new ItemDetail_ItemDTO(Item);
        }


        [Route(ItemDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ItemDetail_ItemDTO>> Create([FromBody] ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = ConvertDTOToEntity(ItemDetail_ItemDTO);

            Item = await ItemService.Create(Item);
            ItemDetail_ItemDTO = new ItemDetail_ItemDTO(Item);
            if (Item.IsValidated)
                return ItemDetail_ItemDTO;
            else
                return BadRequest(ItemDetail_ItemDTO);        
        }

        [Route(ItemDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ItemDetail_ItemDTO>> Update([FromBody] ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = ConvertDTOToEntity(ItemDetail_ItemDTO);

            Item = await ItemService.Update(Item);
            ItemDetail_ItemDTO = new ItemDetail_ItemDTO(Item);
            if (Item.IsValidated)
                return ItemDetail_ItemDTO;
            else
                return BadRequest(ItemDetail_ItemDTO);        
        }

        [Route(ItemDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ItemDetail_ItemDTO>> Delete([FromBody] ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = ConvertDTOToEntity(ItemDetail_ItemDTO);

            Item = await ItemService.Delete(Item);
            ItemDetail_ItemDTO = new ItemDetail_ItemDTO(Item);
            if (Item.IsValidated)
                return ItemDetail_ItemDTO;
            else
                return BadRequest(ItemDetail_ItemDTO);        
        }

        public Item ConvertDTOToEntity(ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            Item Item = new Item();
            
            Item.Id = ItemDetail_ItemDTO.Id;
            Item.Code = ItemDetail_ItemDTO.Code;
            Item.Name = ItemDetail_ItemDTO.Name;
            return Item;
        }
        
        
    }
}

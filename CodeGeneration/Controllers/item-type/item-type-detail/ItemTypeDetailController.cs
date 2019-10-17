

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItemType;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.item_type.item_type_detail
{
    public class ItemTypeDetailRoute : Root
    {
        public const string FE = "/item-type/item-type-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ItemTypeDetailController : ApiController
    {
        
        
        private IItemTypeService ItemTypeService;

        public ItemTypeDetailController(
            
            IItemTypeService ItemTypeService
        )
        {
            
            this.ItemTypeService = ItemTypeService;
        }


        [Route(ItemTypeDetailRoute.Get), HttpPost]
        public async Task<ItemTypeDetail_ItemTypeDTO> Get([FromBody]ItemTypeDetail_ItemTypeDTO ItemTypeDetail_ItemTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemType ItemType = await ItemTypeService.Get(ItemTypeDetail_ItemTypeDTO.Id);
            return new ItemTypeDetail_ItemTypeDTO(ItemType);
        }


        [Route(ItemTypeDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ItemTypeDetail_ItemTypeDTO>> Create([FromBody] ItemTypeDetail_ItemTypeDTO ItemTypeDetail_ItemTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemType ItemType = ConvertDTOToEntity(ItemTypeDetail_ItemTypeDTO);

            ItemType = await ItemTypeService.Create(ItemType);
            ItemTypeDetail_ItemTypeDTO = new ItemTypeDetail_ItemTypeDTO(ItemType);
            if (ItemType.IsValidated)
                return ItemTypeDetail_ItemTypeDTO;
            else
                return BadRequest(ItemTypeDetail_ItemTypeDTO);        
        }

        [Route(ItemTypeDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ItemTypeDetail_ItemTypeDTO>> Update([FromBody] ItemTypeDetail_ItemTypeDTO ItemTypeDetail_ItemTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemType ItemType = ConvertDTOToEntity(ItemTypeDetail_ItemTypeDTO);

            ItemType = await ItemTypeService.Update(ItemType);
            ItemTypeDetail_ItemTypeDTO = new ItemTypeDetail_ItemTypeDTO(ItemType);
            if (ItemType.IsValidated)
                return ItemTypeDetail_ItemTypeDTO;
            else
                return BadRequest(ItemTypeDetail_ItemTypeDTO);        
        }

        [Route(ItemTypeDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ItemTypeDetail_ItemTypeDTO>> Delete([FromBody] ItemTypeDetail_ItemTypeDTO ItemTypeDetail_ItemTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemType ItemType = ConvertDTOToEntity(ItemTypeDetail_ItemTypeDTO);

            ItemType = await ItemTypeService.Delete(ItemType);
            ItemTypeDetail_ItemTypeDTO = new ItemTypeDetail_ItemTypeDTO(ItemType);
            if (ItemType.IsValidated)
                return ItemTypeDetail_ItemTypeDTO;
            else
                return BadRequest(ItemTypeDetail_ItemTypeDTO);        
        }

        public ItemType ConvertDTOToEntity(ItemTypeDetail_ItemTypeDTO ItemTypeDetail_ItemTypeDTO)
        {
            ItemType ItemType = new ItemType();
            
            ItemType.Id = ItemTypeDetail_ItemTypeDTO.Id;
            ItemType.Code = ItemTypeDetail_ItemTypeDTO.Code;
            ItemType.Name = ItemTypeDetail_ItemTypeDTO.Name;
            return ItemType;
        }
        
        
    }
}

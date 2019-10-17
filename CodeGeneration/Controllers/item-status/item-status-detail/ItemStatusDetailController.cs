

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItemStatus;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.item_status.item_status_detail
{
    public class ItemStatusDetailRoute : Root
    {
        public const string FE = "/item-status/item-status-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ItemStatusDetailController : ApiController
    {
        
        
        private IItemStatusService ItemStatusService;

        public ItemStatusDetailController(
            
            IItemStatusService ItemStatusService
        )
        {
            
            this.ItemStatusService = ItemStatusService;
        }


        [Route(ItemStatusDetailRoute.Get), HttpPost]
        public async Task<ItemStatusDetail_ItemStatusDTO> Get([FromBody]ItemStatusDetail_ItemStatusDTO ItemStatusDetail_ItemStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStatus ItemStatus = await ItemStatusService.Get(ItemStatusDetail_ItemStatusDTO.Id);
            return new ItemStatusDetail_ItemStatusDTO(ItemStatus);
        }


        [Route(ItemStatusDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ItemStatusDetail_ItemStatusDTO>> Create([FromBody] ItemStatusDetail_ItemStatusDTO ItemStatusDetail_ItemStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStatus ItemStatus = ConvertDTOToEntity(ItemStatusDetail_ItemStatusDTO);

            ItemStatus = await ItemStatusService.Create(ItemStatus);
            ItemStatusDetail_ItemStatusDTO = new ItemStatusDetail_ItemStatusDTO(ItemStatus);
            if (ItemStatus.IsValidated)
                return ItemStatusDetail_ItemStatusDTO;
            else
                return BadRequest(ItemStatusDetail_ItemStatusDTO);        
        }

        [Route(ItemStatusDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ItemStatusDetail_ItemStatusDTO>> Update([FromBody] ItemStatusDetail_ItemStatusDTO ItemStatusDetail_ItemStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStatus ItemStatus = ConvertDTOToEntity(ItemStatusDetail_ItemStatusDTO);

            ItemStatus = await ItemStatusService.Update(ItemStatus);
            ItemStatusDetail_ItemStatusDTO = new ItemStatusDetail_ItemStatusDTO(ItemStatus);
            if (ItemStatus.IsValidated)
                return ItemStatusDetail_ItemStatusDTO;
            else
                return BadRequest(ItemStatusDetail_ItemStatusDTO);        
        }

        [Route(ItemStatusDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ItemStatusDetail_ItemStatusDTO>> Delete([FromBody] ItemStatusDetail_ItemStatusDTO ItemStatusDetail_ItemStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStatus ItemStatus = ConvertDTOToEntity(ItemStatusDetail_ItemStatusDTO);

            ItemStatus = await ItemStatusService.Delete(ItemStatus);
            ItemStatusDetail_ItemStatusDTO = new ItemStatusDetail_ItemStatusDTO(ItemStatus);
            if (ItemStatus.IsValidated)
                return ItemStatusDetail_ItemStatusDTO;
            else
                return BadRequest(ItemStatusDetail_ItemStatusDTO);        
        }

        public ItemStatus ConvertDTOToEntity(ItemStatusDetail_ItemStatusDTO ItemStatusDetail_ItemStatusDTO)
        {
            ItemStatus ItemStatus = new ItemStatus();
            
            ItemStatus.Id = ItemStatusDetail_ItemStatusDTO.Id;
            ItemStatus.Code = ItemStatusDetail_ItemStatusDTO.Code;
            ItemStatus.Name = ItemStatusDetail_ItemStatusDTO.Name;
            return ItemStatus;
        }
        
        
    }
}

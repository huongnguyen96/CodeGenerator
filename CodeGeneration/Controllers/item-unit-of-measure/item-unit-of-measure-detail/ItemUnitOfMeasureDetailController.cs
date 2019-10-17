

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItemUnitOfMeasure;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.item_unit_of_measure.item_unit_of_measure_detail
{
    public class ItemUnitOfMeasureDetailRoute : Root
    {
        public const string FE = "/item-unit-of-measure/item-unit-of-measure-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ItemUnitOfMeasureDetailController : ApiController
    {
        
        
        private IItemUnitOfMeasureService ItemUnitOfMeasureService;

        public ItemUnitOfMeasureDetailController(
            
            IItemUnitOfMeasureService ItemUnitOfMeasureService
        )
        {
            
            this.ItemUnitOfMeasureService = ItemUnitOfMeasureService;
        }


        [Route(ItemUnitOfMeasureDetailRoute.Get), HttpPost]
        public async Task<ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO> Get([FromBody]ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemUnitOfMeasure ItemUnitOfMeasure = await ItemUnitOfMeasureService.Get(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO.Id);
            return new ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO(ItemUnitOfMeasure);
        }


        [Route(ItemUnitOfMeasureDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO>> Create([FromBody] ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemUnitOfMeasure ItemUnitOfMeasure = ConvertDTOToEntity(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO);

            ItemUnitOfMeasure = await ItemUnitOfMeasureService.Create(ItemUnitOfMeasure);
            ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO = new ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO(ItemUnitOfMeasure);
            if (ItemUnitOfMeasure.IsValidated)
                return ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO;
            else
                return BadRequest(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO);        
        }

        [Route(ItemUnitOfMeasureDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO>> Update([FromBody] ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemUnitOfMeasure ItemUnitOfMeasure = ConvertDTOToEntity(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO);

            ItemUnitOfMeasure = await ItemUnitOfMeasureService.Update(ItemUnitOfMeasure);
            ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO = new ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO(ItemUnitOfMeasure);
            if (ItemUnitOfMeasure.IsValidated)
                return ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO;
            else
                return BadRequest(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO);        
        }

        [Route(ItemUnitOfMeasureDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO>> Delete([FromBody] ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemUnitOfMeasure ItemUnitOfMeasure = ConvertDTOToEntity(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO);

            ItemUnitOfMeasure = await ItemUnitOfMeasureService.Delete(ItemUnitOfMeasure);
            ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO = new ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO(ItemUnitOfMeasure);
            if (ItemUnitOfMeasure.IsValidated)
                return ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO;
            else
                return BadRequest(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO);        
        }

        public ItemUnitOfMeasure ConvertDTOToEntity(ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO)
        {
            ItemUnitOfMeasure ItemUnitOfMeasure = new ItemUnitOfMeasure();
            
            ItemUnitOfMeasure.Id = ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO.Id;
            ItemUnitOfMeasure.Code = ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO.Code;
            ItemUnitOfMeasure.Name = ItemUnitOfMeasureDetail_ItemUnitOfMeasureDTO.Name;
            return ItemUnitOfMeasure;
        }
        
        
    }
}

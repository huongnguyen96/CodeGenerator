

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItemUnitOfMeasure;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.item_unit_of_measure.item_unit_of_measure_master
{
    public class ItemUnitOfMeasureMasterRoute : Root
    {
        public const string FE = "/item-unit-of-measure/item-unit-of-measure-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class ItemUnitOfMeasureMasterController : ApiController
    {
        
        
        private IItemUnitOfMeasureService ItemUnitOfMeasureService;

        public ItemUnitOfMeasureMasterController(
            
            IItemUnitOfMeasureService ItemUnitOfMeasureService
        )
        {
            
            this.ItemUnitOfMeasureService = ItemUnitOfMeasureService;
        }


        [Route(ItemUnitOfMeasureMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = ConvertFilterDTOToFilterEntity(ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO);

            return await ItemUnitOfMeasureService.Count(ItemUnitOfMeasureFilter);
        }

        [Route(ItemUnitOfMeasureMasterRoute.List), HttpPost]
        public async Task<List<ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO>> List([FromBody] ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = ConvertFilterDTOToFilterEntity(ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO);

            List<ItemUnitOfMeasure> ItemUnitOfMeasures = await ItemUnitOfMeasureService.List(ItemUnitOfMeasureFilter);

            return ItemUnitOfMeasures.Select(c => new ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO(c)).ToList();
        }

        [Route(ItemUnitOfMeasureMasterRoute.Get), HttpPost]
        public async Task<ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO> Get([FromBody]ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemUnitOfMeasure ItemUnitOfMeasure = await ItemUnitOfMeasureService.Get(ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO.Id);
            return new ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO(ItemUnitOfMeasure);
        }


        public ItemUnitOfMeasureFilter ConvertFilterDTOToFilterEntity(ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO)
        {
            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = new ItemUnitOfMeasureFilter();
            
            ItemUnitOfMeasureFilter.Id = new LongFilter{ Equal = ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO.Id };
            ItemUnitOfMeasureFilter.Code = new StringFilter{ StartsWith = ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO.Code };
            ItemUnitOfMeasureFilter.Name = new StringFilter{ StartsWith = ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO.Name };
            return ItemUnitOfMeasureFilter;
        }
        
        
    }
}

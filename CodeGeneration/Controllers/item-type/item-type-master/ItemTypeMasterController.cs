

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItemType;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.item_type.item_type_master
{
    public class ItemTypeMasterRoute : Root
    {
        public const string FE = "/item-type/item-type-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class ItemTypeMasterController : ApiController
    {
        
        
        private IItemTypeService ItemTypeService;

        public ItemTypeMasterController(
            
            IItemTypeService ItemTypeService
        )
        {
            
            this.ItemTypeService = ItemTypeService;
        }


        [Route(ItemTypeMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ItemTypeMaster_ItemTypeFilterDTO ItemTypeMaster_ItemTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemTypeFilter ItemTypeFilter = ConvertFilterDTOToFilterEntity(ItemTypeMaster_ItemTypeFilterDTO);

            return await ItemTypeService.Count(ItemTypeFilter);
        }

        [Route(ItemTypeMasterRoute.List), HttpPost]
        public async Task<List<ItemTypeMaster_ItemTypeDTO>> List([FromBody] ItemTypeMaster_ItemTypeFilterDTO ItemTypeMaster_ItemTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemTypeFilter ItemTypeFilter = ConvertFilterDTOToFilterEntity(ItemTypeMaster_ItemTypeFilterDTO);

            List<ItemType> ItemTypes = await ItemTypeService.List(ItemTypeFilter);

            return ItemTypes.Select(c => new ItemTypeMaster_ItemTypeDTO(c)).ToList();
        }

        [Route(ItemTypeMasterRoute.Get), HttpPost]
        public async Task<ItemTypeMaster_ItemTypeDTO> Get([FromBody]ItemTypeMaster_ItemTypeDTO ItemTypeMaster_ItemTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemType ItemType = await ItemTypeService.Get(ItemTypeMaster_ItemTypeDTO.Id);
            return new ItemTypeMaster_ItemTypeDTO(ItemType);
        }


        public ItemTypeFilter ConvertFilterDTOToFilterEntity(ItemTypeMaster_ItemTypeFilterDTO ItemTypeMaster_ItemTypeFilterDTO)
        {
            ItemTypeFilter ItemTypeFilter = new ItemTypeFilter();
            
            ItemTypeFilter.Id = new LongFilter{ Equal = ItemTypeMaster_ItemTypeFilterDTO.Id };
            ItemTypeFilter.Code = new StringFilter{ StartsWith = ItemTypeMaster_ItemTypeFilterDTO.Code };
            ItemTypeFilter.Name = new StringFilter{ StartsWith = ItemTypeMaster_ItemTypeFilterDTO.Name };
            return ItemTypeFilter;
        }
        
        
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItemStatus;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.item_status.item_status_master
{
    public class ItemStatusMasterRoute : Root
    {
        public const string FE = "/item-status/item-status-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class ItemStatusMasterController : ApiController
    {
        
        
        private IItemStatusService ItemStatusService;

        public ItemStatusMasterController(
            
            IItemStatusService ItemStatusService
        )
        {
            
            this.ItemStatusService = ItemStatusService;
        }


        [Route(ItemStatusMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ItemStatusMaster_ItemStatusFilterDTO ItemStatusMaster_ItemStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStatusFilter ItemStatusFilter = ConvertFilterDTOToFilterEntity(ItemStatusMaster_ItemStatusFilterDTO);

            return await ItemStatusService.Count(ItemStatusFilter);
        }

        [Route(ItemStatusMasterRoute.List), HttpPost]
        public async Task<List<ItemStatusMaster_ItemStatusDTO>> List([FromBody] ItemStatusMaster_ItemStatusFilterDTO ItemStatusMaster_ItemStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStatusFilter ItemStatusFilter = ConvertFilterDTOToFilterEntity(ItemStatusMaster_ItemStatusFilterDTO);

            List<ItemStatus> ItemStatuss = await ItemStatusService.List(ItemStatusFilter);

            return ItemStatuss.Select(c => new ItemStatusMaster_ItemStatusDTO(c)).ToList();
        }

        [Route(ItemStatusMasterRoute.Get), HttpPost]
        public async Task<ItemStatusMaster_ItemStatusDTO> Get([FromBody]ItemStatusMaster_ItemStatusDTO ItemStatusMaster_ItemStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStatus ItemStatus = await ItemStatusService.Get(ItemStatusMaster_ItemStatusDTO.Id);
            return new ItemStatusMaster_ItemStatusDTO(ItemStatus);
        }


        public ItemStatusFilter ConvertFilterDTOToFilterEntity(ItemStatusMaster_ItemStatusFilterDTO ItemStatusMaster_ItemStatusFilterDTO)
        {
            ItemStatusFilter ItemStatusFilter = new ItemStatusFilter();
            ItemStatusFilter.Selects = ItemStatusSelect.ALL;
            
            ItemStatusFilter.Id = new LongFilter{ Equal = ItemStatusMaster_ItemStatusFilterDTO.Id };
            ItemStatusFilter.Code = new StringFilter{ StartsWith = ItemStatusMaster_ItemStatusFilterDTO.Code };
            ItemStatusFilter.Name = new StringFilter{ StartsWith = ItemStatusMaster_ItemStatusFilterDTO.Name };
            return ItemStatusFilter;
        }
        
        
    }
}

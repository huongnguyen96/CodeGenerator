
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;

namespace WeGift.Controllers.warehouse.warehouse_list
{
    public class WarehouseListRoute : Root
    {
        public const string FE = "warehouse/warehouse-list";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
    }

    public class WarehouseListController : ApiController
    {
        private IWarehouseService WarehouseService;

        public WarehouseListController(
            IWarehouseService WarehouseService
        )
        {
            this.WarehouseService = WarehouseService;
        }

        [Route(WarehouseListRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] WarehouseList_WarehouseFilterDTO WarehouseList_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOtoFilterEntity(WarehouseList_WarehouseFilterDTO);

            return await WarehouseService.Count(WarehouseFilter);
        }

        [Route(WarehouseListRoute.List), HttpPost]
        public async Task<List<WarehouseList_WarehouseDTO>> List([FromBody] WarehouseList_WarehouseFilterDTO WarehouseList_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOtoFilterEntity(WarehouseList_WarehouseFilterDTO);

            List<Warehouse> Warehouses = await WarehouseService.List(WarehouseFilter);

            return Warehouses.Select(c => new WarehouseList_WarehouseDTO(c)).ToList();
        }

        [Route(WarehouseListRoute.Get), HttpPost]
        public async Task<WarehouseList_WarehouseDTO> Get([FromBody]WarehouseList_WarehouseDTO WarehouseList_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = await WarehouseService.Get(WarehouseList_WarehouseDTO.Id);
            return new WarehouseList_WarehouseDTO(Warehouse);
        }


        public WarehouseFilter ConvertFilterDTOtoFilterEntity(WarehouseList_WarehouseFilterDTO WarehouseList_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            
            WarehouseFilter.Id = WarehouseList_WarehouseFilterDTO.Id;
            WarehouseFilter.ManagerId = WarehouseList_WarehouseFilterDTO.ManagerId;
            WarehouseFilter.Code = WarehouseList_WarehouseFilterDTO.Code;
            WarehouseFilter.Name = WarehouseList_WarehouseFilterDTO.Name;
            WarehouseFilter.Manager = WarehouseList_WarehouseFilterDTO.Manager;
            return WarehouseFilter;
        }
    }
}

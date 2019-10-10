
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;

namespace WeGift.Controllers.warehouse.warehouse_master
{
    public class WarehouseMasterRoute : Root
    {
        public const string FE = "warehouse/warehouse-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
    }

    public class WarehouseMasterController : ApiController
    {
        private IWarehouseService WarehouseService;

        public WarehouseMasterController(
            IWarehouseService WarehouseService
        )
        {
            this.WarehouseService = WarehouseService;
        }

        [Route(WarehouseMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOtoFilterEntity(WarehouseMaster_WarehouseFilterDTO);

            return await WarehouseService.Count(WarehouseFilter);
        }

        [Route(WarehouseMasterRoute.List), HttpPost]
        public async Task<List<WarehouseMaster_WarehouseDTO>> List([FromBody] WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOtoFilterEntity(WarehouseMaster_WarehouseFilterDTO);

            List<Warehouse> Warehouses = await WarehouseService.List(WarehouseFilter);

            return Warehouses.Select(c => new WarehouseMaster_WarehouseDTO(c)).ToList();
        }

        [Route(WarehouseMasterRoute.Get), HttpPost]
        public async Task<WarehouseMaster_WarehouseDTO> Get([FromBody]WarehouseMaster_WarehouseDTO WarehouseMaster_WarehouseDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Warehouse Warehouse = await WarehouseService.Get(WarehouseMaster_WarehouseDTO.Id);
            return new WarehouseMaster_WarehouseDTO(Warehouse);
        }


        public WarehouseFilter ConvertFilterDTOtoFilterEntity(WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            
            WarehouseFilter.Id = WarehouseMaster_WarehouseFilterDTO.Id;
            WarehouseFilter.ManagerId = WarehouseMaster_WarehouseFilterDTO.ManagerId;
            WarehouseFilter.Code = WarehouseMaster_WarehouseFilterDTO.Code;
            WarehouseFilter.Name = WarehouseMaster_WarehouseFilterDTO.Name;
            return WarehouseFilter;
        }
    }
}

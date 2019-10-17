

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MSupplier;


namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMasterRoute : Root
    {
        public const string FE = "/warehouse/warehouse-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListSupplier="/single-list-supplier";
    }

    public class WarehouseMasterController : ApiController
    {
        
        
        private ISupplierService SupplierService;
        private IWarehouseService WarehouseService;

        public WarehouseMasterController(
            
            ISupplierService SupplierService,
            IWarehouseService WarehouseService
        )
        {
            
            this.SupplierService = SupplierService;
            this.WarehouseService = WarehouseService;
        }


        [Route(WarehouseMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOToFilterEntity(WarehouseMaster_WarehouseFilterDTO);

            return await WarehouseService.Count(WarehouseFilter);
        }

        [Route(WarehouseMasterRoute.List), HttpPost]
        public async Task<List<WarehouseMaster_WarehouseDTO>> List([FromBody] WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            WarehouseFilter WarehouseFilter = ConvertFilterDTOToFilterEntity(WarehouseMaster_WarehouseFilterDTO);

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


        public WarehouseFilter ConvertFilterDTOToFilterEntity(WarehouseMaster_WarehouseFilterDTO WarehouseMaster_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            
            WarehouseFilter.Id = new LongFilter{ Equal = WarehouseMaster_WarehouseFilterDTO.Id };
            WarehouseFilter.Name = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Name };
            WarehouseFilter.Phone = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Phone };
            WarehouseFilter.Email = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Email };
            WarehouseFilter.Address = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Address };
            WarehouseFilter.SupplierId = new LongFilter{ Equal = WarehouseMaster_WarehouseFilterDTO.SupplierId };
            return WarehouseFilter;
        }
        
        
        [Route(WarehouseMasterRoute.SingleListSupplier), HttpPost]
        public async Task<List<WarehouseMaster_SupplierDTO>> SingleListSupplier([FromBody] WarehouseMaster_SupplierFilterDTO WarehouseMaster_SupplierFilterDTO)
        {
            SupplierFilter SupplierFilter = new SupplierFilter();
            SupplierFilter.Skip = 0;
            SupplierFilter.Take = 20;
            SupplierFilter.OrderBy = SupplierOrder.Id;
            SupplierFilter.OrderType = OrderType.ASC;
            SupplierFilter.Selects = SupplierSelect.ALL;
            
            SupplierFilter.Id = new LongFilter{ Equal = WarehouseMaster_SupplierFilterDTO.Id };
            SupplierFilter.Name = new StringFilter{ StartsWith = WarehouseMaster_SupplierFilterDTO.Name };
            SupplierFilter.Phone = new StringFilter{ StartsWith = WarehouseMaster_SupplierFilterDTO.Phone };
            SupplierFilter.ContactPerson = new StringFilter{ StartsWith = WarehouseMaster_SupplierFilterDTO.ContactPerson };
            SupplierFilter.Address = new StringFilter{ StartsWith = WarehouseMaster_SupplierFilterDTO.Address };

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<WarehouseMaster_SupplierDTO> WarehouseMaster_SupplierDTOs = Suppliers
                .Select(x => new WarehouseMaster_SupplierDTO(x)).ToList();
            return WarehouseMaster_SupplierDTOs;
        }

    }
}

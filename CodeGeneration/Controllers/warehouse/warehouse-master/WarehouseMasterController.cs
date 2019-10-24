

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MMerchant;


namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMasterRoute : Root
    {
        public const string FE = "/warehouse/warehouse-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListMerchant= Default + "/single-list-merchant";
    }

    public class WarehouseMasterController : ApiController
    {
        
        
        private IMerchantService MerchantService;
        private IWarehouseService WarehouseService;

        public WarehouseMasterController(
            
            IMerchantService MerchantService,
            IWarehouseService WarehouseService
        )
        {
            
            this.MerchantService = MerchantService;
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
            WarehouseFilter.Selects = WarehouseSelect.ALL;
            
            WarehouseFilter.Id = new LongFilter{ Equal = WarehouseMaster_WarehouseFilterDTO.Id };
            WarehouseFilter.Name = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Name };
            WarehouseFilter.Phone = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Phone };
            WarehouseFilter.Email = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Email };
            WarehouseFilter.Address = new StringFilter{ StartsWith = WarehouseMaster_WarehouseFilterDTO.Address };
            WarehouseFilter.PartnerId = new LongFilter{ Equal = WarehouseMaster_WarehouseFilterDTO.PartnerId };
            return WarehouseFilter;
        }
        
        
        [Route(WarehouseMasterRoute.SingleListMerchant), HttpPost]
        public async Task<List<WarehouseMaster_MerchantDTO>> SingleListMerchant([FromBody] WarehouseMaster_MerchantFilterDTO WarehouseMaster_MerchantFilterDTO)
        {
            MerchantFilter MerchantFilter = new MerchantFilter();
            MerchantFilter.Skip = 0;
            MerchantFilter.Take = 20;
            MerchantFilter.OrderBy = MerchantOrder.Id;
            MerchantFilter.OrderType = OrderType.ASC;
            MerchantFilter.Selects = MerchantSelect.ALL;
            
            MerchantFilter.Id = new LongFilter{ Equal = WarehouseMaster_MerchantFilterDTO.Id };
            MerchantFilter.Name = new StringFilter{ StartsWith = WarehouseMaster_MerchantFilterDTO.Name };
            MerchantFilter.Phone = new StringFilter{ StartsWith = WarehouseMaster_MerchantFilterDTO.Phone };
            MerchantFilter.ContactPerson = new StringFilter{ StartsWith = WarehouseMaster_MerchantFilterDTO.ContactPerson };
            MerchantFilter.Address = new StringFilter{ StartsWith = WarehouseMaster_MerchantFilterDTO.Address };

            List<Merchant> Merchants = await MerchantService.List(MerchantFilter);
            List<WarehouseMaster_MerchantDTO> WarehouseMaster_MerchantDTOs = Merchants
                .Select(x => new WarehouseMaster_MerchantDTO(x)).ToList();
            return WarehouseMaster_MerchantDTOs;
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MWarehouse;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MPartner;


namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMasterRoute : Root
    {
        public const string FE = "/warehouse/warehouse-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListPartner="/single-list-partner";
    }

    public class WarehouseMasterController : ApiController
    {
        
        
        private IPartnerService PartnerService;
        private IWarehouseService WarehouseService;

        public WarehouseMasterController(
            
            IPartnerService PartnerService,
            IWarehouseService WarehouseService
        )
        {
            
            this.PartnerService = PartnerService;
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
            WarehouseFilter.PartnerId = new LongFilter{ Equal = WarehouseMaster_WarehouseFilterDTO.PartnerId };
            return WarehouseFilter;
        }
        
        
        [Route(WarehouseMasterRoute.SingleListPartner), HttpPost]
        public async Task<List<WarehouseMaster_PartnerDTO>> SingleListPartner([FromBody] WarehouseMaster_PartnerFilterDTO WarehouseMaster_PartnerFilterDTO)
        {
            PartnerFilter PartnerFilter = new PartnerFilter();
            PartnerFilter.Skip = 0;
            PartnerFilter.Take = 20;
            PartnerFilter.OrderBy = PartnerOrder.Id;
            PartnerFilter.OrderType = OrderType.ASC;
            PartnerFilter.Selects = PartnerSelect.ALL;
            
            PartnerFilter.Id = new LongFilter{ Equal = WarehouseMaster_PartnerFilterDTO.Id };
            PartnerFilter.Name = new StringFilter{ StartsWith = WarehouseMaster_PartnerFilterDTO.Name };
            PartnerFilter.Phone = new StringFilter{ StartsWith = WarehouseMaster_PartnerFilterDTO.Phone };
            PartnerFilter.ContactPerson = new StringFilter{ StartsWith = WarehouseMaster_PartnerFilterDTO.ContactPerson };
            PartnerFilter.Address = new StringFilter{ StartsWith = WarehouseMaster_PartnerFilterDTO.Address };

            List<Partner> Partners = await PartnerService.List(PartnerFilter);
            List<WarehouseMaster_PartnerDTO> WarehouseMaster_PartnerDTOs = Partners
                .Select(x => new WarehouseMaster_PartnerDTO(x)).ToList();
            return WarehouseMaster_PartnerDTOs;
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MStock;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MUnit;
using WG.Services.MWarehouse;


namespace WG.Controllers.stock.stock_master
{
    public class StockMasterRoute : Root
    {
        public const string FE = "/stock/stock-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListUnit="/single-list-unit";
        public const string SingleListWarehouse="/single-list-warehouse";
    }

    public class StockMasterController : ApiController
    {
        
        
        private IUnitService UnitService;
        private IWarehouseService WarehouseService;
        private IStockService StockService;

        public StockMasterController(
            
            IUnitService UnitService,
            IWarehouseService WarehouseService,
            IStockService StockService
        )
        {
            
            this.UnitService = UnitService;
            this.WarehouseService = WarehouseService;
            this.StockService = StockService;
        }


        [Route(StockMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] StockMaster_StockFilterDTO StockMaster_StockFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            StockFilter StockFilter = ConvertFilterDTOToFilterEntity(StockMaster_StockFilterDTO);

            return await StockService.Count(StockFilter);
        }

        [Route(StockMasterRoute.List), HttpPost]
        public async Task<List<StockMaster_StockDTO>> List([FromBody] StockMaster_StockFilterDTO StockMaster_StockFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            StockFilter StockFilter = ConvertFilterDTOToFilterEntity(StockMaster_StockFilterDTO);

            List<Stock> Stocks = await StockService.List(StockFilter);

            return Stocks.Select(c => new StockMaster_StockDTO(c)).ToList();
        }

        [Route(StockMasterRoute.Get), HttpPost]
        public async Task<StockMaster_StockDTO> Get([FromBody]StockMaster_StockDTO StockMaster_StockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Stock Stock = await StockService.Get(StockMaster_StockDTO.Id);
            return new StockMaster_StockDTO(Stock);
        }


        public StockFilter ConvertFilterDTOToFilterEntity(StockMaster_StockFilterDTO StockMaster_StockFilterDTO)
        {
            StockFilter StockFilter = new StockFilter();
            
            StockFilter.Id = new LongFilter{ Equal = StockMaster_StockFilterDTO.Id };
            StockFilter.UnitId = new LongFilter{ Equal = StockMaster_StockFilterDTO.UnitId };
            StockFilter.WarehouseId = new LongFilter{ Equal = StockMaster_StockFilterDTO.WarehouseId };
            StockFilter.Quantity = new LongFilter{ Equal = StockMaster_StockFilterDTO.Quantity };
            return StockFilter;
        }
        
        
        [Route(StockMasterRoute.SingleListUnit), HttpPost]
        public async Task<List<StockMaster_UnitDTO>> SingleListUnit([FromBody] StockMaster_UnitFilterDTO StockMaster_UnitFilterDTO)
        {
            UnitFilter UnitFilter = new UnitFilter();
            UnitFilter.Skip = 0;
            UnitFilter.Take = 20;
            UnitFilter.OrderBy = UnitOrder.Id;
            UnitFilter.OrderType = OrderType.ASC;
            UnitFilter.Selects = UnitSelect.ALL;
            
            UnitFilter.Id = new LongFilter{ Equal = StockMaster_UnitFilterDTO.Id };
            UnitFilter.FirstVariationId = new LongFilter{ Equal = StockMaster_UnitFilterDTO.FirstVariationId };
            UnitFilter.SecondVariationId = new LongFilter{ Equal = StockMaster_UnitFilterDTO.SecondVariationId };
            UnitFilter.ThirdVariationId = new LongFilter{ Equal = StockMaster_UnitFilterDTO.ThirdVariationId };
            UnitFilter.SKU = new StringFilter{ StartsWith = StockMaster_UnitFilterDTO.SKU };
            UnitFilter.Price = new LongFilter{ Equal = StockMaster_UnitFilterDTO.Price };

            List<Unit> Units = await UnitService.List(UnitFilter);
            List<StockMaster_UnitDTO> StockMaster_UnitDTOs = Units
                .Select(x => new StockMaster_UnitDTO(x)).ToList();
            return StockMaster_UnitDTOs;
        }

        [Route(StockMasterRoute.SingleListWarehouse), HttpPost]
        public async Task<List<StockMaster_WarehouseDTO>> SingleListWarehouse([FromBody] StockMaster_WarehouseFilterDTO StockMaster_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            WarehouseFilter.Skip = 0;
            WarehouseFilter.Take = 20;
            WarehouseFilter.OrderBy = WarehouseOrder.Id;
            WarehouseFilter.OrderType = OrderType.ASC;
            WarehouseFilter.Selects = WarehouseSelect.ALL;
            
            WarehouseFilter.Id = new LongFilter{ Equal = StockMaster_WarehouseFilterDTO.Id };
            WarehouseFilter.Name = new StringFilter{ StartsWith = StockMaster_WarehouseFilterDTO.Name };
            WarehouseFilter.Phone = new StringFilter{ StartsWith = StockMaster_WarehouseFilterDTO.Phone };
            WarehouseFilter.Email = new StringFilter{ StartsWith = StockMaster_WarehouseFilterDTO.Email };
            WarehouseFilter.Address = new StringFilter{ StartsWith = StockMaster_WarehouseFilterDTO.Address };
            WarehouseFilter.PartnerId = new LongFilter{ Equal = StockMaster_WarehouseFilterDTO.PartnerId };

            List<Warehouse> Warehouses = await WarehouseService.List(WarehouseFilter);
            List<StockMaster_WarehouseDTO> StockMaster_WarehouseDTOs = Warehouses
                .Select(x => new StockMaster_WarehouseDTO(x)).ToList();
            return StockMaster_WarehouseDTOs;
        }

    }
}

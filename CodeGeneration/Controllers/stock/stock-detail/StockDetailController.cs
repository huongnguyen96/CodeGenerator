

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


namespace WG.Controllers.stock.stock_detail
{
    public class StockDetailRoute : Root
    {
        public const string FE = "/stock/stock-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListUnit= Default + "/single-list-unit";
        public const string SingleListWarehouse= Default + "/single-list-warehouse";
    }

    public class StockDetailController : ApiController
    {
        
        
        private IUnitService UnitService;
        private IWarehouseService WarehouseService;
        private IStockService StockService;

        public StockDetailController(
            
            IUnitService UnitService,
            IWarehouseService WarehouseService,
            IStockService StockService
        )
        {
            
            this.UnitService = UnitService;
            this.WarehouseService = WarehouseService;
            this.StockService = StockService;
        }


        [Route(StockDetailRoute.Get), HttpPost]
        public async Task<StockDetail_StockDTO> Get([FromBody]StockDetail_StockDTO StockDetail_StockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Stock Stock = await StockService.Get(StockDetail_StockDTO.Id);
            return new StockDetail_StockDTO(Stock);
        }


        [Route(StockDetailRoute.Create), HttpPost]
        public async Task<ActionResult<StockDetail_StockDTO>> Create([FromBody] StockDetail_StockDTO StockDetail_StockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Stock Stock = ConvertDTOToEntity(StockDetail_StockDTO);

            Stock = await StockService.Create(Stock);
            StockDetail_StockDTO = new StockDetail_StockDTO(Stock);
            if (Stock.IsValidated)
                return StockDetail_StockDTO;
            else
                return BadRequest(StockDetail_StockDTO);        
        }

        [Route(StockDetailRoute.Update), HttpPost]
        public async Task<ActionResult<StockDetail_StockDTO>> Update([FromBody] StockDetail_StockDTO StockDetail_StockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Stock Stock = ConvertDTOToEntity(StockDetail_StockDTO);

            Stock = await StockService.Update(Stock);
            StockDetail_StockDTO = new StockDetail_StockDTO(Stock);
            if (Stock.IsValidated)
                return StockDetail_StockDTO;
            else
                return BadRequest(StockDetail_StockDTO);        
        }

        [Route(StockDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<StockDetail_StockDTO>> Delete([FromBody] StockDetail_StockDTO StockDetail_StockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Stock Stock = ConvertDTOToEntity(StockDetail_StockDTO);

            Stock = await StockService.Delete(Stock);
            StockDetail_StockDTO = new StockDetail_StockDTO(Stock);
            if (Stock.IsValidated)
                return StockDetail_StockDTO;
            else
                return BadRequest(StockDetail_StockDTO);        
        }

        public Stock ConvertDTOToEntity(StockDetail_StockDTO StockDetail_StockDTO)
        {
            Stock Stock = new Stock();
            
            Stock.Id = StockDetail_StockDTO.Id;
            Stock.UnitId = StockDetail_StockDTO.UnitId;
            Stock.WarehouseId = StockDetail_StockDTO.WarehouseId;
            Stock.Quantity = StockDetail_StockDTO.Quantity;
            return Stock;
        }
        
        
        [Route(StockDetailRoute.SingleListUnit), HttpPost]
        public async Task<List<StockDetail_UnitDTO>> SingleListUnit([FromBody] StockDetail_UnitFilterDTO StockDetail_UnitFilterDTO)
        {
            UnitFilter UnitFilter = new UnitFilter();
            UnitFilter.Skip = 0;
            UnitFilter.Take = 20;
            UnitFilter.OrderBy = UnitOrder.Id;
            UnitFilter.OrderType = OrderType.ASC;
            UnitFilter.Selects = UnitSelect.ALL;
            
            UnitFilter.Id = new LongFilter{ Equal = StockDetail_UnitFilterDTO.Id };
            UnitFilter.FirstVariationId = new LongFilter{ Equal = StockDetail_UnitFilterDTO.FirstVariationId };
            UnitFilter.SecondVariationId = new LongFilter{ Equal = StockDetail_UnitFilterDTO.SecondVariationId };
            UnitFilter.ThirdVariationId = new LongFilter{ Equal = StockDetail_UnitFilterDTO.ThirdVariationId };
            UnitFilter.SKU = new StringFilter{ StartsWith = StockDetail_UnitFilterDTO.SKU };
            UnitFilter.Price = new LongFilter{ Equal = StockDetail_UnitFilterDTO.Price };

            List<Unit> Units = await UnitService.List(UnitFilter);
            List<StockDetail_UnitDTO> StockDetail_UnitDTOs = Units
                .Select(x => new StockDetail_UnitDTO(x)).ToList();
            return StockDetail_UnitDTOs;
        }

        [Route(StockDetailRoute.SingleListWarehouse), HttpPost]
        public async Task<List<StockDetail_WarehouseDTO>> SingleListWarehouse([FromBody] StockDetail_WarehouseFilterDTO StockDetail_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            WarehouseFilter.Skip = 0;
            WarehouseFilter.Take = 20;
            WarehouseFilter.OrderBy = WarehouseOrder.Id;
            WarehouseFilter.OrderType = OrderType.ASC;
            WarehouseFilter.Selects = WarehouseSelect.ALL;
            
            WarehouseFilter.Id = new LongFilter{ Equal = StockDetail_WarehouseFilterDTO.Id };
            WarehouseFilter.Name = new StringFilter{ StartsWith = StockDetail_WarehouseFilterDTO.Name };
            WarehouseFilter.Phone = new StringFilter{ StartsWith = StockDetail_WarehouseFilterDTO.Phone };
            WarehouseFilter.Email = new StringFilter{ StartsWith = StockDetail_WarehouseFilterDTO.Email };
            WarehouseFilter.Address = new StringFilter{ StartsWith = StockDetail_WarehouseFilterDTO.Address };
            WarehouseFilter.PartnerId = new LongFilter{ Equal = StockDetail_WarehouseFilterDTO.PartnerId };

            List<Warehouse> Warehouses = await WarehouseService.List(WarehouseFilter);
            List<StockDetail_WarehouseDTO> StockDetail_WarehouseDTOs = Warehouses
                .Select(x => new StockDetail_WarehouseDTO(x)).ToList();
            return StockDetail_WarehouseDTOs;
        }

    }
}

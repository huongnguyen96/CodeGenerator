

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MStock;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MItem;
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
        
        public const string SingleListItem= Default + "/single-list-item";
        public const string SingleListWarehouse= Default + "/single-list-warehouse";
    }

    public class StockMasterController : ApiController
    {
        
        
        private IItemService ItemService;
        private IWarehouseService WarehouseService;
        private IStockService StockService;

        public StockMasterController(
            
            IItemService ItemService,
            IWarehouseService WarehouseService,
            IStockService StockService
        )
        {
            
            this.ItemService = ItemService;
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
            StockFilter.Selects = StockSelect.ALL;
            
            StockFilter.Id = new LongFilter{ Equal = StockMaster_StockFilterDTO.Id };
            StockFilter.ItemId = new LongFilter{ Equal = StockMaster_StockFilterDTO.ItemId };
            StockFilter.WarehouseId = new LongFilter{ Equal = StockMaster_StockFilterDTO.WarehouseId };
            StockFilter.Quantity = new LongFilter{ Equal = StockMaster_StockFilterDTO.Quantity };
            return StockFilter;
        }
        
        
        [Route(StockMasterRoute.SingleListItem), HttpPost]
        public async Task<List<StockMaster_ItemDTO>> SingleListItem([FromBody] StockMaster_ItemFilterDTO StockMaster_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = StockMaster_ItemFilterDTO.Id };
            ItemFilter.ProductId = new LongFilter{ Equal = StockMaster_ItemFilterDTO.ProductId };
            ItemFilter.FirstVariationId = new LongFilter{ Equal = StockMaster_ItemFilterDTO.FirstVariationId };
            ItemFilter.SecondVariationId = new LongFilter{ Equal = StockMaster_ItemFilterDTO.SecondVariationId };
            ItemFilter.SKU = new StringFilter{ StartsWith = StockMaster_ItemFilterDTO.SKU };
            ItemFilter.Price = new LongFilter{ Equal = StockMaster_ItemFilterDTO.Price };
            ItemFilter.MinPrice = new LongFilter{ Equal = StockMaster_ItemFilterDTO.MinPrice };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<StockMaster_ItemDTO> StockMaster_ItemDTOs = Items
                .Select(x => new StockMaster_ItemDTO(x)).ToList();
            return StockMaster_ItemDTOs;
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

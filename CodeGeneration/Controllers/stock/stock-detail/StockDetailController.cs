

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
        
        public const string SingleListItem= Default + "/single-list-item";
        public const string SingleListWarehouse= Default + "/single-list-warehouse";
    }

    public class StockDetailController : ApiController
    {
        
        
        private IItemService ItemService;
        private IWarehouseService WarehouseService;
        private IStockService StockService;

        public StockDetailController(
            
            IItemService ItemService,
            IWarehouseService WarehouseService,
            IStockService StockService
        )
        {
            
            this.ItemService = ItemService;
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
            Stock.ItemId = StockDetail_StockDTO.ItemId;
            Stock.WarehouseId = StockDetail_StockDTO.WarehouseId;
            Stock.Quantity = StockDetail_StockDTO.Quantity;
            return Stock;
        }
        
        
        [Route(StockDetailRoute.SingleListItem), HttpPost]
        public async Task<List<StockDetail_ItemDTO>> SingleListItem([FromBody] StockDetail_ItemFilterDTO StockDetail_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = StockDetail_ItemFilterDTO.Id };
            ItemFilter.ProductId = new LongFilter{ Equal = StockDetail_ItemFilterDTO.ProductId };
            ItemFilter.FirstVariationId = new LongFilter{ Equal = StockDetail_ItemFilterDTO.FirstVariationId };
            ItemFilter.SecondVariationId = new LongFilter{ Equal = StockDetail_ItemFilterDTO.SecondVariationId };
            ItemFilter.SKU = new StringFilter{ StartsWith = StockDetail_ItemFilterDTO.SKU };
            ItemFilter.Price = new LongFilter{ Equal = StockDetail_ItemFilterDTO.Price };
            ItemFilter.MinPrice = new LongFilter{ Equal = StockDetail_ItemFilterDTO.MinPrice };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<StockDetail_ItemDTO> StockDetail_ItemDTOs = Items
                .Select(x => new StockDetail_ItemDTO(x)).ToList();
            return StockDetail_ItemDTOs;
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

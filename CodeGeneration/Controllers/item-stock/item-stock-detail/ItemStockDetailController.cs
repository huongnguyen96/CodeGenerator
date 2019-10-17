

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItemStock;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MItem;
using WG.Services.MItemUnitOfMeasure;
using WG.Services.MWarehouse;


namespace WG.Controllers.item_stock.item_stock_detail
{
    public class ItemStockDetailRoute : Root
    {
        public const string FE = "/item-stock/item-stock-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListItem="/single-list-item";
        public const string SingleListItemUnitOfMeasure="/single-list-item-unit-of-measure";
        public const string SingleListWarehouse="/single-list-warehouse";
    }

    public class ItemStockDetailController : ApiController
    {
        
        
        private IItemService ItemService;
        private IItemUnitOfMeasureService ItemUnitOfMeasureService;
        private IWarehouseService WarehouseService;
        private IItemStockService ItemStockService;

        public ItemStockDetailController(
            
            IItemService ItemService,
            IItemUnitOfMeasureService ItemUnitOfMeasureService,
            IWarehouseService WarehouseService,
            IItemStockService ItemStockService
        )
        {
            
            this.ItemService = ItemService;
            this.ItemUnitOfMeasureService = ItemUnitOfMeasureService;
            this.WarehouseService = WarehouseService;
            this.ItemStockService = ItemStockService;
        }


        [Route(ItemStockDetailRoute.Get), HttpPost]
        public async Task<ItemStockDetail_ItemStockDTO> Get([FromBody]ItemStockDetail_ItemStockDTO ItemStockDetail_ItemStockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStock ItemStock = await ItemStockService.Get(ItemStockDetail_ItemStockDTO.Id);
            return new ItemStockDetail_ItemStockDTO(ItemStock);
        }


        [Route(ItemStockDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ItemStockDetail_ItemStockDTO>> Create([FromBody] ItemStockDetail_ItemStockDTO ItemStockDetail_ItemStockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStock ItemStock = ConvertDTOToEntity(ItemStockDetail_ItemStockDTO);

            ItemStock = await ItemStockService.Create(ItemStock);
            ItemStockDetail_ItemStockDTO = new ItemStockDetail_ItemStockDTO(ItemStock);
            if (ItemStock.IsValidated)
                return ItemStockDetail_ItemStockDTO;
            else
                return BadRequest(ItemStockDetail_ItemStockDTO);        
        }

        [Route(ItemStockDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ItemStockDetail_ItemStockDTO>> Update([FromBody] ItemStockDetail_ItemStockDTO ItemStockDetail_ItemStockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStock ItemStock = ConvertDTOToEntity(ItemStockDetail_ItemStockDTO);

            ItemStock = await ItemStockService.Update(ItemStock);
            ItemStockDetail_ItemStockDTO = new ItemStockDetail_ItemStockDTO(ItemStock);
            if (ItemStock.IsValidated)
                return ItemStockDetail_ItemStockDTO;
            else
                return BadRequest(ItemStockDetail_ItemStockDTO);        
        }

        [Route(ItemStockDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ItemStockDetail_ItemStockDTO>> Delete([FromBody] ItemStockDetail_ItemStockDTO ItemStockDetail_ItemStockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStock ItemStock = ConvertDTOToEntity(ItemStockDetail_ItemStockDTO);

            ItemStock = await ItemStockService.Delete(ItemStock);
            ItemStockDetail_ItemStockDTO = new ItemStockDetail_ItemStockDTO(ItemStock);
            if (ItemStock.IsValidated)
                return ItemStockDetail_ItemStockDTO;
            else
                return BadRequest(ItemStockDetail_ItemStockDTO);        
        }

        public ItemStock ConvertDTOToEntity(ItemStockDetail_ItemStockDTO ItemStockDetail_ItemStockDTO)
        {
            ItemStock ItemStock = new ItemStock();
            
            ItemStock.Id = ItemStockDetail_ItemStockDTO.Id;
            ItemStock.ItemId = ItemStockDetail_ItemStockDTO.ItemId;
            ItemStock.WarehouseId = ItemStockDetail_ItemStockDTO.WarehouseId;
            ItemStock.UnitOfMeasureId = ItemStockDetail_ItemStockDTO.UnitOfMeasureId;
            ItemStock.Quantity = ItemStockDetail_ItemStockDTO.Quantity;
            return ItemStock;
        }
        
        
        [Route(ItemStockDetailRoute.SingleListItem), HttpPost]
        public async Task<List<ItemStockDetail_ItemDTO>> SingleListItem([FromBody] ItemStockDetail_ItemFilterDTO ItemStockDetail_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = ItemStockDetail_ItemFilterDTO.Id };
            ItemFilter.Code = new StringFilter{ StartsWith = ItemStockDetail_ItemFilterDTO.Code };
            ItemFilter.Name = new StringFilter{ StartsWith = ItemStockDetail_ItemFilterDTO.Name };
            ItemFilter.SKU = new StringFilter{ StartsWith = ItemStockDetail_ItemFilterDTO.SKU };
            ItemFilter.TypeId = new LongFilter{ Equal = ItemStockDetail_ItemFilterDTO.TypeId };
            ItemFilter.PurchasePrice = new DecimalFilter{ Equal = ItemStockDetail_ItemFilterDTO.PurchasePrice };
            ItemFilter.SalePrice = new DecimalFilter{ Equal = ItemStockDetail_ItemFilterDTO.SalePrice };
            ItemFilter.Description = new StringFilter{ StartsWith = ItemStockDetail_ItemFilterDTO.Description };
            ItemFilter.StatusId = new LongFilter{ Equal = ItemStockDetail_ItemFilterDTO.StatusId };
            ItemFilter.UnitOfMeasureId = new LongFilter{ Equal = ItemStockDetail_ItemFilterDTO.UnitOfMeasureId };
            ItemFilter.SupplierId = new LongFilter{ Equal = ItemStockDetail_ItemFilterDTO.SupplierId };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<ItemStockDetail_ItemDTO> ItemStockDetail_ItemDTOs = Items
                .Select(x => new ItemStockDetail_ItemDTO(x)).ToList();
            return ItemStockDetail_ItemDTOs;
        }

        [Route(ItemStockDetailRoute.SingleListItemUnitOfMeasure), HttpPost]
        public async Task<List<ItemStockDetail_ItemUnitOfMeasureDTO>> SingleListItemUnitOfMeasure([FromBody] ItemStockDetail_ItemUnitOfMeasureFilterDTO ItemStockDetail_ItemUnitOfMeasureFilterDTO)
        {
            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = new ItemUnitOfMeasureFilter();
            ItemUnitOfMeasureFilter.Skip = 0;
            ItemUnitOfMeasureFilter.Take = 20;
            ItemUnitOfMeasureFilter.OrderBy = ItemUnitOfMeasureOrder.Id;
            ItemUnitOfMeasureFilter.OrderType = OrderType.ASC;
            ItemUnitOfMeasureFilter.Selects = ItemUnitOfMeasureSelect.ALL;
            
            ItemUnitOfMeasureFilter.Id = new LongFilter{ Equal = ItemStockDetail_ItemUnitOfMeasureFilterDTO.Id };
            ItemUnitOfMeasureFilter.Code = new StringFilter{ StartsWith = ItemStockDetail_ItemUnitOfMeasureFilterDTO.Code };
            ItemUnitOfMeasureFilter.Name = new StringFilter{ StartsWith = ItemStockDetail_ItemUnitOfMeasureFilterDTO.Name };

            List<ItemUnitOfMeasure> ItemUnitOfMeasures = await ItemUnitOfMeasureService.List(ItemUnitOfMeasureFilter);
            List<ItemStockDetail_ItemUnitOfMeasureDTO> ItemStockDetail_ItemUnitOfMeasureDTOs = ItemUnitOfMeasures
                .Select(x => new ItemStockDetail_ItemUnitOfMeasureDTO(x)).ToList();
            return ItemStockDetail_ItemUnitOfMeasureDTOs;
        }

        [Route(ItemStockDetailRoute.SingleListWarehouse), HttpPost]
        public async Task<List<ItemStockDetail_WarehouseDTO>> SingleListWarehouse([FromBody] ItemStockDetail_WarehouseFilterDTO ItemStockDetail_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            WarehouseFilter.Skip = 0;
            WarehouseFilter.Take = 20;
            WarehouseFilter.OrderBy = WarehouseOrder.Id;
            WarehouseFilter.OrderType = OrderType.ASC;
            WarehouseFilter.Selects = WarehouseSelect.ALL;
            
            WarehouseFilter.Id = new LongFilter{ Equal = ItemStockDetail_WarehouseFilterDTO.Id };
            WarehouseFilter.Name = new StringFilter{ StartsWith = ItemStockDetail_WarehouseFilterDTO.Name };
            WarehouseFilter.Phone = new StringFilter{ StartsWith = ItemStockDetail_WarehouseFilterDTO.Phone };
            WarehouseFilter.Email = new StringFilter{ StartsWith = ItemStockDetail_WarehouseFilterDTO.Email };
            WarehouseFilter.Address = new StringFilter{ StartsWith = ItemStockDetail_WarehouseFilterDTO.Address };
            WarehouseFilter.SupplierId = new LongFilter{ Equal = ItemStockDetail_WarehouseFilterDTO.SupplierId };

            List<Warehouse> Warehouses = await WarehouseService.List(WarehouseFilter);
            List<ItemStockDetail_WarehouseDTO> ItemStockDetail_WarehouseDTOs = Warehouses
                .Select(x => new ItemStockDetail_WarehouseDTO(x)).ToList();
            return ItemStockDetail_WarehouseDTOs;
        }

    }
}

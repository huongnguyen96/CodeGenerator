

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


namespace WG.Controllers.item_stock.item_stock_master
{
    public class ItemStockMasterRoute : Root
    {
        public const string FE = "/item-stock/item-stock-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListItem="/single-list-item";
        public const string SingleListItemUnitOfMeasure="/single-list-item-unit-of-measure";
        public const string SingleListWarehouse="/single-list-warehouse";
    }

    public class ItemStockMasterController : ApiController
    {
        
        
        private IItemService ItemService;
        private IItemUnitOfMeasureService ItemUnitOfMeasureService;
        private IWarehouseService WarehouseService;
        private IItemStockService ItemStockService;

        public ItemStockMasterController(
            
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


        [Route(ItemStockMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ItemStockMaster_ItemStockFilterDTO ItemStockMaster_ItemStockFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStockFilter ItemStockFilter = ConvertFilterDTOToFilterEntity(ItemStockMaster_ItemStockFilterDTO);

            return await ItemStockService.Count(ItemStockFilter);
        }

        [Route(ItemStockMasterRoute.List), HttpPost]
        public async Task<List<ItemStockMaster_ItemStockDTO>> List([FromBody] ItemStockMaster_ItemStockFilterDTO ItemStockMaster_ItemStockFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStockFilter ItemStockFilter = ConvertFilterDTOToFilterEntity(ItemStockMaster_ItemStockFilterDTO);

            List<ItemStock> ItemStocks = await ItemStockService.List(ItemStockFilter);

            return ItemStocks.Select(c => new ItemStockMaster_ItemStockDTO(c)).ToList();
        }

        [Route(ItemStockMasterRoute.Get), HttpPost]
        public async Task<ItemStockMaster_ItemStockDTO> Get([FromBody]ItemStockMaster_ItemStockDTO ItemStockMaster_ItemStockDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemStock ItemStock = await ItemStockService.Get(ItemStockMaster_ItemStockDTO.Id);
            return new ItemStockMaster_ItemStockDTO(ItemStock);
        }


        public ItemStockFilter ConvertFilterDTOToFilterEntity(ItemStockMaster_ItemStockFilterDTO ItemStockMaster_ItemStockFilterDTO)
        {
            ItemStockFilter ItemStockFilter = new ItemStockFilter();
            
            ItemStockFilter.Id = new LongFilter{ Equal = ItemStockMaster_ItemStockFilterDTO.Id };
            ItemStockFilter.ItemId = new LongFilter{ Equal = ItemStockMaster_ItemStockFilterDTO.ItemId };
            ItemStockFilter.WarehouseId = new LongFilter{ Equal = ItemStockMaster_ItemStockFilterDTO.WarehouseId };
            ItemStockFilter.UnitOfMeasureId = new LongFilter{ Equal = ItemStockMaster_ItemStockFilterDTO.UnitOfMeasureId };
            ItemStockFilter.Quantity = new DecimalFilter{ Equal = ItemStockMaster_ItemStockFilterDTO.Quantity };
            return ItemStockFilter;
        }
        
        
        [Route(ItemStockMasterRoute.SingleListItem), HttpPost]
        public async Task<List<ItemStockMaster_ItemDTO>> SingleListItem([FromBody] ItemStockMaster_ItemFilterDTO ItemStockMaster_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = ItemStockMaster_ItemFilterDTO.Id };
            ItemFilter.Code = new StringFilter{ StartsWith = ItemStockMaster_ItemFilterDTO.Code };
            ItemFilter.Name = new StringFilter{ StartsWith = ItemStockMaster_ItemFilterDTO.Name };
            ItemFilter.SKU = new StringFilter{ StartsWith = ItemStockMaster_ItemFilterDTO.SKU };
            ItemFilter.TypeId = new LongFilter{ Equal = ItemStockMaster_ItemFilterDTO.TypeId };
            ItemFilter.PurchasePrice = new DecimalFilter{ Equal = ItemStockMaster_ItemFilterDTO.PurchasePrice };
            ItemFilter.SalePrice = new DecimalFilter{ Equal = ItemStockMaster_ItemFilterDTO.SalePrice };
            ItemFilter.Description = new StringFilter{ StartsWith = ItemStockMaster_ItemFilterDTO.Description };
            ItemFilter.StatusId = new LongFilter{ Equal = ItemStockMaster_ItemFilterDTO.StatusId };
            ItemFilter.UnitOfMeasureId = new LongFilter{ Equal = ItemStockMaster_ItemFilterDTO.UnitOfMeasureId };
            ItemFilter.SupplierId = new LongFilter{ Equal = ItemStockMaster_ItemFilterDTO.SupplierId };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<ItemStockMaster_ItemDTO> ItemStockMaster_ItemDTOs = Items
                .Select(x => new ItemStockMaster_ItemDTO(x)).ToList();
            return ItemStockMaster_ItemDTOs;
        }

        [Route(ItemStockMasterRoute.SingleListItemUnitOfMeasure), HttpPost]
        public async Task<List<ItemStockMaster_ItemUnitOfMeasureDTO>> SingleListItemUnitOfMeasure([FromBody] ItemStockMaster_ItemUnitOfMeasureFilterDTO ItemStockMaster_ItemUnitOfMeasureFilterDTO)
        {
            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = new ItemUnitOfMeasureFilter();
            ItemUnitOfMeasureFilter.Skip = 0;
            ItemUnitOfMeasureFilter.Take = 20;
            ItemUnitOfMeasureFilter.OrderBy = ItemUnitOfMeasureOrder.Id;
            ItemUnitOfMeasureFilter.OrderType = OrderType.ASC;
            ItemUnitOfMeasureFilter.Selects = ItemUnitOfMeasureSelect.ALL;
            
            ItemUnitOfMeasureFilter.Id = new LongFilter{ Equal = ItemStockMaster_ItemUnitOfMeasureFilterDTO.Id };
            ItemUnitOfMeasureFilter.Code = new StringFilter{ StartsWith = ItemStockMaster_ItemUnitOfMeasureFilterDTO.Code };
            ItemUnitOfMeasureFilter.Name = new StringFilter{ StartsWith = ItemStockMaster_ItemUnitOfMeasureFilterDTO.Name };

            List<ItemUnitOfMeasure> ItemUnitOfMeasures = await ItemUnitOfMeasureService.List(ItemUnitOfMeasureFilter);
            List<ItemStockMaster_ItemUnitOfMeasureDTO> ItemStockMaster_ItemUnitOfMeasureDTOs = ItemUnitOfMeasures
                .Select(x => new ItemStockMaster_ItemUnitOfMeasureDTO(x)).ToList();
            return ItemStockMaster_ItemUnitOfMeasureDTOs;
        }

        [Route(ItemStockMasterRoute.SingleListWarehouse), HttpPost]
        public async Task<List<ItemStockMaster_WarehouseDTO>> SingleListWarehouse([FromBody] ItemStockMaster_WarehouseFilterDTO ItemStockMaster_WarehouseFilterDTO)
        {
            WarehouseFilter WarehouseFilter = new WarehouseFilter();
            WarehouseFilter.Skip = 0;
            WarehouseFilter.Take = 20;
            WarehouseFilter.OrderBy = WarehouseOrder.Id;
            WarehouseFilter.OrderType = OrderType.ASC;
            WarehouseFilter.Selects = WarehouseSelect.ALL;
            
            WarehouseFilter.Id = new LongFilter{ Equal = ItemStockMaster_WarehouseFilterDTO.Id };
            WarehouseFilter.Name = new StringFilter{ StartsWith = ItemStockMaster_WarehouseFilterDTO.Name };
            WarehouseFilter.Phone = new StringFilter{ StartsWith = ItemStockMaster_WarehouseFilterDTO.Phone };
            WarehouseFilter.Email = new StringFilter{ StartsWith = ItemStockMaster_WarehouseFilterDTO.Email };
            WarehouseFilter.Address = new StringFilter{ StartsWith = ItemStockMaster_WarehouseFilterDTO.Address };
            WarehouseFilter.SupplierId = new LongFilter{ Equal = ItemStockMaster_WarehouseFilterDTO.SupplierId };

            List<Warehouse> Warehouses = await WarehouseService.List(WarehouseFilter);
            List<ItemStockMaster_WarehouseDTO> ItemStockMaster_WarehouseDTOs = Warehouses
                .Select(x => new ItemStockMaster_WarehouseDTO(x)).ToList();
            return ItemStockMaster_WarehouseDTOs;
        }

    }
}

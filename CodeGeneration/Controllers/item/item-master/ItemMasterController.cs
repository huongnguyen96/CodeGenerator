

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItem;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MItemStatus;
using WG.Services.MSupplier;
using WG.Services.MItemType;
using WG.Services.MItemUnitOfMeasure;


namespace WG.Controllers.item.item_master
{
    public class ItemMasterRoute : Root
    {
        public const string FE = "/item/item-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListItemStatus="/single-list-item-status";
        public const string SingleListSupplier="/single-list-supplier";
        public const string SingleListItemType="/single-list-item-type";
        public const string SingleListItemUnitOfMeasure="/single-list-item-unit-of-measure";
    }

    public class ItemMasterController : ApiController
    {
        
        
        private IItemStatusService ItemStatusService;
        private ISupplierService SupplierService;
        private IItemTypeService ItemTypeService;
        private IItemUnitOfMeasureService ItemUnitOfMeasureService;
        private IItemService ItemService;

        public ItemMasterController(
            
            IItemStatusService ItemStatusService,
            ISupplierService SupplierService,
            IItemTypeService ItemTypeService,
            IItemUnitOfMeasureService ItemUnitOfMeasureService,
            IItemService ItemService
        )
        {
            
            this.ItemStatusService = ItemStatusService;
            this.SupplierService = SupplierService;
            this.ItemTypeService = ItemTypeService;
            this.ItemUnitOfMeasureService = ItemUnitOfMeasureService;
            this.ItemService = ItemService;
        }


        [Route(ItemMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ItemMaster_ItemFilterDTO ItemMaster_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemFilter ItemFilter = ConvertFilterDTOToFilterEntity(ItemMaster_ItemFilterDTO);

            return await ItemService.Count(ItemFilter);
        }

        [Route(ItemMasterRoute.List), HttpPost]
        public async Task<List<ItemMaster_ItemDTO>> List([FromBody] ItemMaster_ItemFilterDTO ItemMaster_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ItemFilter ItemFilter = ConvertFilterDTOToFilterEntity(ItemMaster_ItemFilterDTO);

            List<Item> Items = await ItemService.List(ItemFilter);

            return Items.Select(c => new ItemMaster_ItemDTO(c)).ToList();
        }

        [Route(ItemMasterRoute.Get), HttpPost]
        public async Task<ItemMaster_ItemDTO> Get([FromBody]ItemMaster_ItemDTO ItemMaster_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = await ItemService.Get(ItemMaster_ItemDTO.Id);
            return new ItemMaster_ItemDTO(Item);
        }


        public ItemFilter ConvertFilterDTOToFilterEntity(ItemMaster_ItemFilterDTO ItemMaster_ItemFilterDTO)
        {
            ItemFilter ItemFilter = new ItemFilter();
            
            ItemFilter.Id = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.Id };
            ItemFilter.Code = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.Code };
            ItemFilter.Name = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.Name };
            ItemFilter.SKU = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.SKU };
            ItemFilter.TypeId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.TypeId };
            ItemFilter.PurchasePrice = new DecimalFilter{ Equal = ItemMaster_ItemFilterDTO.PurchasePrice };
            ItemFilter.SalePrice = new DecimalFilter{ Equal = ItemMaster_ItemFilterDTO.SalePrice };
            ItemFilter.Description = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.Description };
            ItemFilter.StatusId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.StatusId };
            ItemFilter.UnitOfMeasureId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.UnitOfMeasureId };
            ItemFilter.SupplierId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.SupplierId };
            return ItemFilter;
        }
        
        
        [Route(ItemMasterRoute.SingleListItemStatus), HttpPost]
        public async Task<List<ItemMaster_ItemStatusDTO>> SingleListItemStatus([FromBody] ItemMaster_ItemStatusFilterDTO ItemMaster_ItemStatusFilterDTO)
        {
            ItemStatusFilter ItemStatusFilter = new ItemStatusFilter();
            ItemStatusFilter.Skip = 0;
            ItemStatusFilter.Take = 20;
            ItemStatusFilter.OrderBy = ItemStatusOrder.Id;
            ItemStatusFilter.OrderType = OrderType.ASC;
            ItemStatusFilter.Selects = ItemStatusSelect.ALL;
            
            ItemStatusFilter.Id = new LongFilter{ Equal = ItemMaster_ItemStatusFilterDTO.Id };
            ItemStatusFilter.Code = new StringFilter{ StartsWith = ItemMaster_ItemStatusFilterDTO.Code };
            ItemStatusFilter.Name = new StringFilter{ StartsWith = ItemMaster_ItemStatusFilterDTO.Name };

            List<ItemStatus> ItemStatuss = await ItemStatusService.List(ItemStatusFilter);
            List<ItemMaster_ItemStatusDTO> ItemMaster_ItemStatusDTOs = ItemStatuss
                .Select(x => new ItemMaster_ItemStatusDTO(x)).ToList();
            return ItemMaster_ItemStatusDTOs;
        }

        [Route(ItemMasterRoute.SingleListSupplier), HttpPost]
        public async Task<List<ItemMaster_SupplierDTO>> SingleListSupplier([FromBody] ItemMaster_SupplierFilterDTO ItemMaster_SupplierFilterDTO)
        {
            SupplierFilter SupplierFilter = new SupplierFilter();
            SupplierFilter.Skip = 0;
            SupplierFilter.Take = 20;
            SupplierFilter.OrderBy = SupplierOrder.Id;
            SupplierFilter.OrderType = OrderType.ASC;
            SupplierFilter.Selects = SupplierSelect.ALL;
            
            SupplierFilter.Id = new LongFilter{ Equal = ItemMaster_SupplierFilterDTO.Id };
            SupplierFilter.Name = new StringFilter{ StartsWith = ItemMaster_SupplierFilterDTO.Name };
            SupplierFilter.Phone = new StringFilter{ StartsWith = ItemMaster_SupplierFilterDTO.Phone };
            SupplierFilter.ContactPerson = new StringFilter{ StartsWith = ItemMaster_SupplierFilterDTO.ContactPerson };
            SupplierFilter.Address = new StringFilter{ StartsWith = ItemMaster_SupplierFilterDTO.Address };

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<ItemMaster_SupplierDTO> ItemMaster_SupplierDTOs = Suppliers
                .Select(x => new ItemMaster_SupplierDTO(x)).ToList();
            return ItemMaster_SupplierDTOs;
        }

        [Route(ItemMasterRoute.SingleListItemType), HttpPost]
        public async Task<List<ItemMaster_ItemTypeDTO>> SingleListItemType([FromBody] ItemMaster_ItemTypeFilterDTO ItemMaster_ItemTypeFilterDTO)
        {
            ItemTypeFilter ItemTypeFilter = new ItemTypeFilter();
            ItemTypeFilter.Skip = 0;
            ItemTypeFilter.Take = 20;
            ItemTypeFilter.OrderBy = ItemTypeOrder.Id;
            ItemTypeFilter.OrderType = OrderType.ASC;
            ItemTypeFilter.Selects = ItemTypeSelect.ALL;
            
            ItemTypeFilter.Id = new LongFilter{ Equal = ItemMaster_ItemTypeFilterDTO.Id };
            ItemTypeFilter.Code = new StringFilter{ StartsWith = ItemMaster_ItemTypeFilterDTO.Code };
            ItemTypeFilter.Name = new StringFilter{ StartsWith = ItemMaster_ItemTypeFilterDTO.Name };

            List<ItemType> ItemTypes = await ItemTypeService.List(ItemTypeFilter);
            List<ItemMaster_ItemTypeDTO> ItemMaster_ItemTypeDTOs = ItemTypes
                .Select(x => new ItemMaster_ItemTypeDTO(x)).ToList();
            return ItemMaster_ItemTypeDTOs;
        }

        [Route(ItemMasterRoute.SingleListItemUnitOfMeasure), HttpPost]
        public async Task<List<ItemMaster_ItemUnitOfMeasureDTO>> SingleListItemUnitOfMeasure([FromBody] ItemMaster_ItemUnitOfMeasureFilterDTO ItemMaster_ItemUnitOfMeasureFilterDTO)
        {
            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = new ItemUnitOfMeasureFilter();
            ItemUnitOfMeasureFilter.Skip = 0;
            ItemUnitOfMeasureFilter.Take = 20;
            ItemUnitOfMeasureFilter.OrderBy = ItemUnitOfMeasureOrder.Id;
            ItemUnitOfMeasureFilter.OrderType = OrderType.ASC;
            ItemUnitOfMeasureFilter.Selects = ItemUnitOfMeasureSelect.ALL;
            
            ItemUnitOfMeasureFilter.Id = new LongFilter{ Equal = ItemMaster_ItemUnitOfMeasureFilterDTO.Id };
            ItemUnitOfMeasureFilter.Code = new StringFilter{ StartsWith = ItemMaster_ItemUnitOfMeasureFilterDTO.Code };
            ItemUnitOfMeasureFilter.Name = new StringFilter{ StartsWith = ItemMaster_ItemUnitOfMeasureFilterDTO.Name };

            List<ItemUnitOfMeasure> ItemUnitOfMeasures = await ItemUnitOfMeasureService.List(ItemUnitOfMeasureFilter);
            List<ItemMaster_ItemUnitOfMeasureDTO> ItemMaster_ItemUnitOfMeasureDTOs = ItemUnitOfMeasures
                .Select(x => new ItemMaster_ItemUnitOfMeasureDTO(x)).ToList();
            return ItemMaster_ItemUnitOfMeasureDTOs;
        }

    }
}



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


namespace WG.Controllers.item.item_detail
{
    public class ItemDetailRoute : Root
    {
        public const string FE = "/item/item-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListItemStatus="/single-list-item-status";
        public const string SingleListSupplier="/single-list-supplier";
        public const string SingleListItemType="/single-list-item-type";
        public const string SingleListItemUnitOfMeasure="/single-list-item-unit-of-measure";
    }

    public class ItemDetailController : ApiController
    {
        
        
        private IItemStatusService ItemStatusService;
        private ISupplierService SupplierService;
        private IItemTypeService ItemTypeService;
        private IItemUnitOfMeasureService ItemUnitOfMeasureService;
        private IItemService ItemService;

        public ItemDetailController(
            
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


        [Route(ItemDetailRoute.Get), HttpPost]
        public async Task<ItemDetail_ItemDTO> Get([FromBody]ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = await ItemService.Get(ItemDetail_ItemDTO.Id);
            return new ItemDetail_ItemDTO(Item);
        }


        [Route(ItemDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ItemDetail_ItemDTO>> Create([FromBody] ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = ConvertDTOToEntity(ItemDetail_ItemDTO);

            Item = await ItemService.Create(Item);
            ItemDetail_ItemDTO = new ItemDetail_ItemDTO(Item);
            if (Item.IsValidated)
                return ItemDetail_ItemDTO;
            else
                return BadRequest(ItemDetail_ItemDTO);        
        }

        [Route(ItemDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ItemDetail_ItemDTO>> Update([FromBody] ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = ConvertDTOToEntity(ItemDetail_ItemDTO);

            Item = await ItemService.Update(Item);
            ItemDetail_ItemDTO = new ItemDetail_ItemDTO(Item);
            if (Item.IsValidated)
                return ItemDetail_ItemDTO;
            else
                return BadRequest(ItemDetail_ItemDTO);        
        }

        [Route(ItemDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ItemDetail_ItemDTO>> Delete([FromBody] ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Item Item = ConvertDTOToEntity(ItemDetail_ItemDTO);

            Item = await ItemService.Delete(Item);
            ItemDetail_ItemDTO = new ItemDetail_ItemDTO(Item);
            if (Item.IsValidated)
                return ItemDetail_ItemDTO;
            else
                return BadRequest(ItemDetail_ItemDTO);        
        }

        public Item ConvertDTOToEntity(ItemDetail_ItemDTO ItemDetail_ItemDTO)
        {
            Item Item = new Item();
            
            Item.Id = ItemDetail_ItemDTO.Id;
            Item.Code = ItemDetail_ItemDTO.Code;
            Item.Name = ItemDetail_ItemDTO.Name;
            Item.SKU = ItemDetail_ItemDTO.SKU;
            Item.TypeId = ItemDetail_ItemDTO.TypeId;
            Item.PurchasePrice = ItemDetail_ItemDTO.PurchasePrice;
            Item.SalePrice = ItemDetail_ItemDTO.SalePrice;
            Item.Description = ItemDetail_ItemDTO.Description;
            Item.StatusId = ItemDetail_ItemDTO.StatusId;
            Item.UnitOfMeasureId = ItemDetail_ItemDTO.UnitOfMeasureId;
            Item.SupplierId = ItemDetail_ItemDTO.SupplierId;
            return Item;
        }
        
        
        [Route(ItemDetailRoute.SingleListItemStatus), HttpPost]
        public async Task<List<ItemDetail_ItemStatusDTO>> SingleListItemStatus([FromBody] ItemDetail_ItemStatusFilterDTO ItemDetail_ItemStatusFilterDTO)
        {
            ItemStatusFilter ItemStatusFilter = new ItemStatusFilter();
            ItemStatusFilter.Skip = 0;
            ItemStatusFilter.Take = 20;
            ItemStatusFilter.OrderBy = ItemStatusOrder.Id;
            ItemStatusFilter.OrderType = OrderType.ASC;
            ItemStatusFilter.Selects = ItemStatusSelect.ALL;
            
            ItemStatusFilter.Id = new LongFilter{ Equal = ItemDetail_ItemStatusFilterDTO.Id };
            ItemStatusFilter.Code = new StringFilter{ StartsWith = ItemDetail_ItemStatusFilterDTO.Code };
            ItemStatusFilter.Name = new StringFilter{ StartsWith = ItemDetail_ItemStatusFilterDTO.Name };

            List<ItemStatus> ItemStatuss = await ItemStatusService.List(ItemStatusFilter);
            List<ItemDetail_ItemStatusDTO> ItemDetail_ItemStatusDTOs = ItemStatuss
                .Select(x => new ItemDetail_ItemStatusDTO(x)).ToList();
            return ItemDetail_ItemStatusDTOs;
        }

        [Route(ItemDetailRoute.SingleListSupplier), HttpPost]
        public async Task<List<ItemDetail_SupplierDTO>> SingleListSupplier([FromBody] ItemDetail_SupplierFilterDTO ItemDetail_SupplierFilterDTO)
        {
            SupplierFilter SupplierFilter = new SupplierFilter();
            SupplierFilter.Skip = 0;
            SupplierFilter.Take = 20;
            SupplierFilter.OrderBy = SupplierOrder.Id;
            SupplierFilter.OrderType = OrderType.ASC;
            SupplierFilter.Selects = SupplierSelect.ALL;
            
            SupplierFilter.Id = new LongFilter{ Equal = ItemDetail_SupplierFilterDTO.Id };
            SupplierFilter.Name = new StringFilter{ StartsWith = ItemDetail_SupplierFilterDTO.Name };
            SupplierFilter.Phone = new StringFilter{ StartsWith = ItemDetail_SupplierFilterDTO.Phone };
            SupplierFilter.ContactPerson = new StringFilter{ StartsWith = ItemDetail_SupplierFilterDTO.ContactPerson };
            SupplierFilter.Address = new StringFilter{ StartsWith = ItemDetail_SupplierFilterDTO.Address };

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<ItemDetail_SupplierDTO> ItemDetail_SupplierDTOs = Suppliers
                .Select(x => new ItemDetail_SupplierDTO(x)).ToList();
            return ItemDetail_SupplierDTOs;
        }

        [Route(ItemDetailRoute.SingleListItemType), HttpPost]
        public async Task<List<ItemDetail_ItemTypeDTO>> SingleListItemType([FromBody] ItemDetail_ItemTypeFilterDTO ItemDetail_ItemTypeFilterDTO)
        {
            ItemTypeFilter ItemTypeFilter = new ItemTypeFilter();
            ItemTypeFilter.Skip = 0;
            ItemTypeFilter.Take = 20;
            ItemTypeFilter.OrderBy = ItemTypeOrder.Id;
            ItemTypeFilter.OrderType = OrderType.ASC;
            ItemTypeFilter.Selects = ItemTypeSelect.ALL;
            
            ItemTypeFilter.Id = new LongFilter{ Equal = ItemDetail_ItemTypeFilterDTO.Id };
            ItemTypeFilter.Code = new StringFilter{ StartsWith = ItemDetail_ItemTypeFilterDTO.Code };
            ItemTypeFilter.Name = new StringFilter{ StartsWith = ItemDetail_ItemTypeFilterDTO.Name };

            List<ItemType> ItemTypes = await ItemTypeService.List(ItemTypeFilter);
            List<ItemDetail_ItemTypeDTO> ItemDetail_ItemTypeDTOs = ItemTypes
                .Select(x => new ItemDetail_ItemTypeDTO(x)).ToList();
            return ItemDetail_ItemTypeDTOs;
        }

        [Route(ItemDetailRoute.SingleListItemUnitOfMeasure), HttpPost]
        public async Task<List<ItemDetail_ItemUnitOfMeasureDTO>> SingleListItemUnitOfMeasure([FromBody] ItemDetail_ItemUnitOfMeasureFilterDTO ItemDetail_ItemUnitOfMeasureFilterDTO)
        {
            ItemUnitOfMeasureFilter ItemUnitOfMeasureFilter = new ItemUnitOfMeasureFilter();
            ItemUnitOfMeasureFilter.Skip = 0;
            ItemUnitOfMeasureFilter.Take = 20;
            ItemUnitOfMeasureFilter.OrderBy = ItemUnitOfMeasureOrder.Id;
            ItemUnitOfMeasureFilter.OrderType = OrderType.ASC;
            ItemUnitOfMeasureFilter.Selects = ItemUnitOfMeasureSelect.ALL;
            
            ItemUnitOfMeasureFilter.Id = new LongFilter{ Equal = ItemDetail_ItemUnitOfMeasureFilterDTO.Id };
            ItemUnitOfMeasureFilter.Code = new StringFilter{ StartsWith = ItemDetail_ItemUnitOfMeasureFilterDTO.Code };
            ItemUnitOfMeasureFilter.Name = new StringFilter{ StartsWith = ItemDetail_ItemUnitOfMeasureFilterDTO.Name };

            List<ItemUnitOfMeasure> ItemUnitOfMeasures = await ItemUnitOfMeasureService.List(ItemUnitOfMeasureFilter);
            List<ItemDetail_ItemUnitOfMeasureDTO> ItemDetail_ItemUnitOfMeasureDTOs = ItemUnitOfMeasures
                .Select(x => new ItemDetail_ItemUnitOfMeasureDTO(x)).ToList();
            return ItemDetail_ItemUnitOfMeasureDTOs;
        }

    }
}

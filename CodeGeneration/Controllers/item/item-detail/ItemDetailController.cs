

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItem;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MBrand;
using WG.Services.MCategory;
using WG.Services.MPartner;
using WG.Services.MItemStatus;
using WG.Services.MItemType;


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
        
        public const string SingleListBrand= Default + "/single-list-brand";
        public const string SingleListCategory= Default + "/single-list-category";
        public const string SingleListPartner= Default + "/single-list-partner";
        public const string SingleListItemStatus= Default + "/single-list-item-status";
        public const string SingleListItemType= Default + "/single-list-item-type";
    }

    public class ItemDetailController : ApiController
    {
        
        
        private IBrandService BrandService;
        private ICategoryService CategoryService;
        private IPartnerService PartnerService;
        private IItemStatusService ItemStatusService;
        private IItemTypeService ItemTypeService;
        private IItemService ItemService;

        public ItemDetailController(
            
            IBrandService BrandService,
            ICategoryService CategoryService,
            IPartnerService PartnerService,
            IItemStatusService ItemStatusService,
            IItemTypeService ItemTypeService,
            IItemService ItemService
        )
        {
            
            this.BrandService = BrandService;
            this.CategoryService = CategoryService;
            this.PartnerService = PartnerService;
            this.ItemStatusService = ItemStatusService;
            this.ItemTypeService = ItemTypeService;
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
            Item.Description = ItemDetail_ItemDTO.Description;
            Item.TypeId = ItemDetail_ItemDTO.TypeId;
            Item.StatusId = ItemDetail_ItemDTO.StatusId;
            Item.PartnerId = ItemDetail_ItemDTO.PartnerId;
            Item.CategoryId = ItemDetail_ItemDTO.CategoryId;
            Item.BrandId = ItemDetail_ItemDTO.BrandId;
            return Item;
        }
        
        
        [Route(ItemDetailRoute.SingleListBrand), HttpPost]
        public async Task<List<ItemDetail_BrandDTO>> SingleListBrand([FromBody] ItemDetail_BrandFilterDTO ItemDetail_BrandFilterDTO)
        {
            BrandFilter BrandFilter = new BrandFilter();
            BrandFilter.Skip = 0;
            BrandFilter.Take = 20;
            BrandFilter.OrderBy = BrandOrder.Id;
            BrandFilter.OrderType = OrderType.ASC;
            BrandFilter.Selects = BrandSelect.ALL;
            
            BrandFilter.Id = new LongFilter{ Equal = ItemDetail_BrandFilterDTO.Id };
            BrandFilter.Name = new StringFilter{ StartsWith = ItemDetail_BrandFilterDTO.Name };
            BrandFilter.CategoryId = new LongFilter{ Equal = ItemDetail_BrandFilterDTO.CategoryId };

            List<Brand> Brands = await BrandService.List(BrandFilter);
            List<ItemDetail_BrandDTO> ItemDetail_BrandDTOs = Brands
                .Select(x => new ItemDetail_BrandDTO(x)).ToList();
            return ItemDetail_BrandDTOs;
        }

        [Route(ItemDetailRoute.SingleListCategory), HttpPost]
        public async Task<List<ItemDetail_CategoryDTO>> SingleListCategory([FromBody] ItemDetail_CategoryFilterDTO ItemDetail_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = ItemDetail_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = ItemDetail_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = ItemDetail_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = ItemDetail_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = ItemDetail_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<ItemDetail_CategoryDTO> ItemDetail_CategoryDTOs = Categorys
                .Select(x => new ItemDetail_CategoryDTO(x)).ToList();
            return ItemDetail_CategoryDTOs;
        }

        [Route(ItemDetailRoute.SingleListPartner), HttpPost]
        public async Task<List<ItemDetail_PartnerDTO>> SingleListPartner([FromBody] ItemDetail_PartnerFilterDTO ItemDetail_PartnerFilterDTO)
        {
            PartnerFilter PartnerFilter = new PartnerFilter();
            PartnerFilter.Skip = 0;
            PartnerFilter.Take = 20;
            PartnerFilter.OrderBy = PartnerOrder.Id;
            PartnerFilter.OrderType = OrderType.ASC;
            PartnerFilter.Selects = PartnerSelect.ALL;
            
            PartnerFilter.Id = new LongFilter{ Equal = ItemDetail_PartnerFilterDTO.Id };
            PartnerFilter.Name = new StringFilter{ StartsWith = ItemDetail_PartnerFilterDTO.Name };
            PartnerFilter.Phone = new StringFilter{ StartsWith = ItemDetail_PartnerFilterDTO.Phone };
            PartnerFilter.ContactPerson = new StringFilter{ StartsWith = ItemDetail_PartnerFilterDTO.ContactPerson };
            PartnerFilter.Address = new StringFilter{ StartsWith = ItemDetail_PartnerFilterDTO.Address };

            List<Partner> Partners = await PartnerService.List(PartnerFilter);
            List<ItemDetail_PartnerDTO> ItemDetail_PartnerDTOs = Partners
                .Select(x => new ItemDetail_PartnerDTO(x)).ToList();
            return ItemDetail_PartnerDTOs;
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

    }
}

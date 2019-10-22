

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


namespace WG.Controllers.item.item_master
{
    public class ItemMasterRoute : Root
    {
        public const string FE = "/item/item-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListBrand="/single-list-brand";
        public const string SingleListCategory="/single-list-category";
        public const string SingleListPartner="/single-list-partner";
        public const string SingleListItemStatus="/single-list-item-status";
        public const string SingleListItemType="/single-list-item-type";
    }

    public class ItemMasterController : ApiController
    {
        
        
        private IBrandService BrandService;
        private ICategoryService CategoryService;
        private IPartnerService PartnerService;
        private IItemStatusService ItemStatusService;
        private IItemTypeService ItemTypeService;
        private IItemService ItemService;

        public ItemMasterController(
            
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
            ItemFilter.Selects = ItemSelect.ALL;
            
            ItemFilter.Id = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.Id };
            ItemFilter.Code = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.Code };
            ItemFilter.Name = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.Name };
            ItemFilter.SKU = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.SKU };
            ItemFilter.Description = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.Description };
            ItemFilter.TypeId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.TypeId };
            ItemFilter.StatusId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.StatusId };
            ItemFilter.PartnerId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.PartnerId };
            ItemFilter.CategoryId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.CategoryId };
            ItemFilter.BrandId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.BrandId };
            return ItemFilter;
        }
        
        
        [Route(ItemMasterRoute.SingleListBrand), HttpPost]
        public async Task<List<ItemMaster_BrandDTO>> SingleListBrand([FromBody] ItemMaster_BrandFilterDTO ItemMaster_BrandFilterDTO)
        {
            BrandFilter BrandFilter = new BrandFilter();
            BrandFilter.Skip = 0;
            BrandFilter.Take = 20;
            BrandFilter.OrderBy = BrandOrder.Id;
            BrandFilter.OrderType = OrderType.ASC;
            BrandFilter.Selects = BrandSelect.ALL;
            
            BrandFilter.Id = new LongFilter{ Equal = ItemMaster_BrandFilterDTO.Id };
            BrandFilter.Name = new StringFilter{ StartsWith = ItemMaster_BrandFilterDTO.Name };
            BrandFilter.CategoryId = new LongFilter{ Equal = ItemMaster_BrandFilterDTO.CategoryId };

            List<Brand> Brands = await BrandService.List(BrandFilter);
            List<ItemMaster_BrandDTO> ItemMaster_BrandDTOs = Brands
                .Select(x => new ItemMaster_BrandDTO(x)).ToList();
            return ItemMaster_BrandDTOs;
        }

        [Route(ItemMasterRoute.SingleListCategory), HttpPost]
        public async Task<List<ItemMaster_CategoryDTO>> SingleListCategory([FromBody] ItemMaster_CategoryFilterDTO ItemMaster_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = ItemMaster_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = ItemMaster_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = ItemMaster_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = ItemMaster_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = ItemMaster_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<ItemMaster_CategoryDTO> ItemMaster_CategoryDTOs = Categorys
                .Select(x => new ItemMaster_CategoryDTO(x)).ToList();
            return ItemMaster_CategoryDTOs;
        }

        [Route(ItemMasterRoute.SingleListPartner), HttpPost]
        public async Task<List<ItemMaster_PartnerDTO>> SingleListPartner([FromBody] ItemMaster_PartnerFilterDTO ItemMaster_PartnerFilterDTO)
        {
            PartnerFilter PartnerFilter = new PartnerFilter();
            PartnerFilter.Skip = 0;
            PartnerFilter.Take = 20;
            PartnerFilter.OrderBy = PartnerOrder.Id;
            PartnerFilter.OrderType = OrderType.ASC;
            PartnerFilter.Selects = PartnerSelect.ALL;
            
            PartnerFilter.Id = new LongFilter{ Equal = ItemMaster_PartnerFilterDTO.Id };
            PartnerFilter.Name = new StringFilter{ StartsWith = ItemMaster_PartnerFilterDTO.Name };
            PartnerFilter.Phone = new StringFilter{ StartsWith = ItemMaster_PartnerFilterDTO.Phone };
            PartnerFilter.ContactPerson = new StringFilter{ StartsWith = ItemMaster_PartnerFilterDTO.ContactPerson };
            PartnerFilter.Address = new StringFilter{ StartsWith = ItemMaster_PartnerFilterDTO.Address };

            List<Partner> Partners = await PartnerService.List(PartnerFilter);
            List<ItemMaster_PartnerDTO> ItemMaster_PartnerDTOs = Partners
                .Select(x => new ItemMaster_PartnerDTO(x)).ToList();
            return ItemMaster_PartnerDTOs;
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

    }
}

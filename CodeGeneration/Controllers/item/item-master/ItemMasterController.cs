

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MItem;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MVariation;
using WG.Services.MProduct;
using WG.Services.MVariation;


namespace WG.Controllers.item.item_master
{
    public class ItemMasterRoute : Root
    {
        public const string FE = "/item/item-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListVariation= Default + "/single-list-variation";
        public const string SingleListProduct= Default + "/single-list-product";
    }

    public class ItemMasterController : ApiController
    {
        
        
        private IVariationService VariationService;
        private IProductService ProductService;
        private IItemService ItemService;

        public ItemMasterController(
            
            IVariationService VariationService,
            IProductService ProductService,
            IItemService ItemService
        )
        {
            
            this.VariationService = VariationService;
            this.ProductService = ProductService;
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
            ItemFilter.ProductId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.ProductId };
            ItemFilter.FirstVariationId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.FirstVariationId };
            ItemFilter.SecondVariationId = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.SecondVariationId };
            ItemFilter.SKU = new StringFilter{ StartsWith = ItemMaster_ItemFilterDTO.SKU };
            ItemFilter.Price = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.Price };
            ItemFilter.MinPrice = new LongFilter{ Equal = ItemMaster_ItemFilterDTO.MinPrice };
            return ItemFilter;
        }
        
        
        [Route(ItemMasterRoute.SingleListVariation), HttpPost]
        public async Task<List<ItemMaster_VariationDTO>> SingleListVariation([FromBody] ItemMaster_VariationFilterDTO ItemMaster_VariationFilterDTO)
        {
            VariationFilter VariationFilter = new VariationFilter();
            VariationFilter.Skip = 0;
            VariationFilter.Take = 20;
            VariationFilter.OrderBy = VariationOrder.Id;
            VariationFilter.OrderType = OrderType.ASC;
            VariationFilter.Selects = VariationSelect.ALL;
            
            VariationFilter.Id = new LongFilter{ Equal = ItemMaster_VariationFilterDTO.Id };
            VariationFilter.Name = new StringFilter{ StartsWith = ItemMaster_VariationFilterDTO.Name };
            VariationFilter.VariationGroupingId = new LongFilter{ Equal = ItemMaster_VariationFilterDTO.VariationGroupingId };

            List<Variation> Variations = await VariationService.List(VariationFilter);
            List<ItemMaster_VariationDTO> ItemMaster_VariationDTOs = Variations
                .Select(x => new ItemMaster_VariationDTO(x)).ToList();
            return ItemMaster_VariationDTOs;
        }

        [Route(ItemMasterRoute.SingleListProduct), HttpPost]
        public async Task<List<ItemMaster_ProductDTO>> SingleListProduct([FromBody] ItemMaster_ProductFilterDTO ItemMaster_ProductFilterDTO)
        {
            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            
            ProductFilter.Id = new LongFilter{ Equal = ItemMaster_ProductFilterDTO.Id };
            ProductFilter.Code = new StringFilter{ StartsWith = ItemMaster_ProductFilterDTO.Code };
            ProductFilter.Name = new StringFilter{ StartsWith = ItemMaster_ProductFilterDTO.Name };
            ProductFilter.Description = new StringFilter{ StartsWith = ItemMaster_ProductFilterDTO.Description };
            ProductFilter.TypeId = new LongFilter{ Equal = ItemMaster_ProductFilterDTO.TypeId };
            ProductFilter.StatusId = new LongFilter{ Equal = ItemMaster_ProductFilterDTO.StatusId };
            ProductFilter.MerchantId = new LongFilter{ Equal = ItemMaster_ProductFilterDTO.MerchantId };
            ProductFilter.CategoryId = new LongFilter{ Equal = ItemMaster_ProductFilterDTO.CategoryId };
            ProductFilter.BrandId = new LongFilter{ Equal = ItemMaster_ProductFilterDTO.BrandId };
            ProductFilter.WarrantyPolicy = new StringFilter{ StartsWith = ItemMaster_ProductFilterDTO.WarrantyPolicy };
            ProductFilter.ReturnPolicy = new StringFilter{ StartsWith = ItemMaster_ProductFilterDTO.ReturnPolicy };
            ProductFilter.ExpiredDate = new StringFilter{ StartsWith = ItemMaster_ProductFilterDTO.ExpiredDate };
            ProductFilter.ConditionOfUse = new StringFilter{ StartsWith = ItemMaster_ProductFilterDTO.ConditionOfUse };
            ProductFilter.MaximumPurchaseQuantity = new LongFilter{ Equal = ItemMaster_ProductFilterDTO.MaximumPurchaseQuantity };

            List<Product> Products = await ProductService.List(ProductFilter);
            List<ItemMaster_ProductDTO> ItemMaster_ProductDTOs = Products
                .Select(x => new ItemMaster_ProductDTO(x)).ToList();
            return ItemMaster_ProductDTOs;
        }

    }
}

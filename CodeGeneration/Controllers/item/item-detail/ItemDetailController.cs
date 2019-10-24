

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
        
        public const string SingleListVariation= Default + "/single-list-variation";
        public const string SingleListProduct= Default + "/single-list-product";
    }

    public class ItemDetailController : ApiController
    {
        
        
        private IVariationService VariationService;
        private IProductService ProductService;
        private IItemService ItemService;

        public ItemDetailController(
            
            IVariationService VariationService,
            IProductService ProductService,
            IItemService ItemService
        )
        {
            
            this.VariationService = VariationService;
            this.ProductService = ProductService;
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
            Item.ProductId = ItemDetail_ItemDTO.ProductId;
            Item.FirstVariationId = ItemDetail_ItemDTO.FirstVariationId;
            Item.SecondVariationId = ItemDetail_ItemDTO.SecondVariationId;
            Item.SKU = ItemDetail_ItemDTO.SKU;
            Item.Price = ItemDetail_ItemDTO.Price;
            Item.MinPrice = ItemDetail_ItemDTO.MinPrice;
            return Item;
        }
        
        
        [Route(ItemDetailRoute.SingleListVariation), HttpPost]
        public async Task<List<ItemDetail_VariationDTO>> SingleListVariation([FromBody] ItemDetail_VariationFilterDTO ItemDetail_VariationFilterDTO)
        {
            VariationFilter VariationFilter = new VariationFilter();
            VariationFilter.Skip = 0;
            VariationFilter.Take = 20;
            VariationFilter.OrderBy = VariationOrder.Id;
            VariationFilter.OrderType = OrderType.ASC;
            VariationFilter.Selects = VariationSelect.ALL;
            
            VariationFilter.Id = new LongFilter{ Equal = ItemDetail_VariationFilterDTO.Id };
            VariationFilter.Name = new StringFilter{ StartsWith = ItemDetail_VariationFilterDTO.Name };
            VariationFilter.VariationGroupingId = new LongFilter{ Equal = ItemDetail_VariationFilterDTO.VariationGroupingId };

            List<Variation> Variations = await VariationService.List(VariationFilter);
            List<ItemDetail_VariationDTO> ItemDetail_VariationDTOs = Variations
                .Select(x => new ItemDetail_VariationDTO(x)).ToList();
            return ItemDetail_VariationDTOs;
        }

        [Route(ItemDetailRoute.SingleListProduct), HttpPost]
        public async Task<List<ItemDetail_ProductDTO>> SingleListProduct([FromBody] ItemDetail_ProductFilterDTO ItemDetail_ProductFilterDTO)
        {
            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            
            ProductFilter.Id = new LongFilter{ Equal = ItemDetail_ProductFilterDTO.Id };
            ProductFilter.Code = new StringFilter{ StartsWith = ItemDetail_ProductFilterDTO.Code };
            ProductFilter.Name = new StringFilter{ StartsWith = ItemDetail_ProductFilterDTO.Name };
            ProductFilter.Description = new StringFilter{ StartsWith = ItemDetail_ProductFilterDTO.Description };
            ProductFilter.TypeId = new LongFilter{ Equal = ItemDetail_ProductFilterDTO.TypeId };
            ProductFilter.StatusId = new LongFilter{ Equal = ItemDetail_ProductFilterDTO.StatusId };
            ProductFilter.MerchantId = new LongFilter{ Equal = ItemDetail_ProductFilterDTO.MerchantId };
            ProductFilter.CategoryId = new LongFilter{ Equal = ItemDetail_ProductFilterDTO.CategoryId };
            ProductFilter.BrandId = new LongFilter{ Equal = ItemDetail_ProductFilterDTO.BrandId };
            ProductFilter.WarrantyPolicy = new StringFilter{ StartsWith = ItemDetail_ProductFilterDTO.WarrantyPolicy };
            ProductFilter.ReturnPolicy = new StringFilter{ StartsWith = ItemDetail_ProductFilterDTO.ReturnPolicy };
            ProductFilter.ExpiredDate = new StringFilter{ StartsWith = ItemDetail_ProductFilterDTO.ExpiredDate };
            ProductFilter.ConditionOfUse = new StringFilter{ StartsWith = ItemDetail_ProductFilterDTO.ConditionOfUse };
            ProductFilter.MaximumPurchaseQuantity = new LongFilter{ Equal = ItemDetail_ProductFilterDTO.MaximumPurchaseQuantity };

            List<Product> Products = await ProductService.List(ProductFilter);
            List<ItemDetail_ProductDTO> ItemDetail_ProductDTOs = Products
                .Select(x => new ItemDetail_ProductDTO(x)).ToList();
            return ItemDetail_ProductDTOs;
        }

    }
}

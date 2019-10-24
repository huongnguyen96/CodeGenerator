

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MVariationGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MProduct;


namespace WG.Controllers.variation_grouping.variation_grouping_detail
{
    public class VariationGroupingDetailRoute : Root
    {
        public const string FE = "/variation-grouping/variation-grouping-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListProduct= Default + "/single-list-product";
    }

    public class VariationGroupingDetailController : ApiController
    {
        
        
        private IProductService ProductService;
        private IVariationGroupingService VariationGroupingService;

        public VariationGroupingDetailController(
            
            IProductService ProductService,
            IVariationGroupingService VariationGroupingService
        )
        {
            
            this.ProductService = ProductService;
            this.VariationGroupingService = VariationGroupingService;
        }


        [Route(VariationGroupingDetailRoute.Get), HttpPost]
        public async Task<VariationGroupingDetail_VariationGroupingDTO> Get([FromBody]VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = await VariationGroupingService.Get(VariationGroupingDetail_VariationGroupingDTO.Id);
            return new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
        }


        [Route(VariationGroupingDetailRoute.Create), HttpPost]
        public async Task<ActionResult<VariationGroupingDetail_VariationGroupingDTO>> Create([FromBody] VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO);

            VariationGrouping = await VariationGroupingService.Create(VariationGrouping);
            VariationGroupingDetail_VariationGroupingDTO = new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
            if (VariationGrouping.IsValidated)
                return VariationGroupingDetail_VariationGroupingDTO;
            else
                return BadRequest(VariationGroupingDetail_VariationGroupingDTO);        
        }

        [Route(VariationGroupingDetailRoute.Update), HttpPost]
        public async Task<ActionResult<VariationGroupingDetail_VariationGroupingDTO>> Update([FromBody] VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO);

            VariationGrouping = await VariationGroupingService.Update(VariationGrouping);
            VariationGroupingDetail_VariationGroupingDTO = new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
            if (VariationGrouping.IsValidated)
                return VariationGroupingDetail_VariationGroupingDTO;
            else
                return BadRequest(VariationGroupingDetail_VariationGroupingDTO);        
        }

        [Route(VariationGroupingDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<VariationGroupingDetail_VariationGroupingDTO>> Delete([FromBody] VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO);

            VariationGrouping = await VariationGroupingService.Delete(VariationGrouping);
            VariationGroupingDetail_VariationGroupingDTO = new VariationGroupingDetail_VariationGroupingDTO(VariationGrouping);
            if (VariationGrouping.IsValidated)
                return VariationGroupingDetail_VariationGroupingDTO;
            else
                return BadRequest(VariationGroupingDetail_VariationGroupingDTO);        
        }

        public VariationGrouping ConvertDTOToEntity(VariationGroupingDetail_VariationGroupingDTO VariationGroupingDetail_VariationGroupingDTO)
        {
            VariationGrouping VariationGrouping = new VariationGrouping();
            
            VariationGrouping.Id = VariationGroupingDetail_VariationGroupingDTO.Id;
            VariationGrouping.Name = VariationGroupingDetail_VariationGroupingDTO.Name;
            VariationGrouping.ProductId = VariationGroupingDetail_VariationGroupingDTO.ProductId;
            return VariationGrouping;
        }
        
        
        [Route(VariationGroupingDetailRoute.SingleListProduct), HttpPost]
        public async Task<List<VariationGroupingDetail_ProductDTO>> SingleListProduct([FromBody] VariationGroupingDetail_ProductFilterDTO VariationGroupingDetail_ProductFilterDTO)
        {
            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            
            ProductFilter.Id = new LongFilter{ Equal = VariationGroupingDetail_ProductFilterDTO.Id };
            ProductFilter.Code = new StringFilter{ StartsWith = VariationGroupingDetail_ProductFilterDTO.Code };
            ProductFilter.Name = new StringFilter{ StartsWith = VariationGroupingDetail_ProductFilterDTO.Name };
            ProductFilter.Description = new StringFilter{ StartsWith = VariationGroupingDetail_ProductFilterDTO.Description };
            ProductFilter.TypeId = new LongFilter{ Equal = VariationGroupingDetail_ProductFilterDTO.TypeId };
            ProductFilter.StatusId = new LongFilter{ Equal = VariationGroupingDetail_ProductFilterDTO.StatusId };
            ProductFilter.MerchantId = new LongFilter{ Equal = VariationGroupingDetail_ProductFilterDTO.MerchantId };
            ProductFilter.CategoryId = new LongFilter{ Equal = VariationGroupingDetail_ProductFilterDTO.CategoryId };
            ProductFilter.BrandId = new LongFilter{ Equal = VariationGroupingDetail_ProductFilterDTO.BrandId };
            ProductFilter.WarrantyPolicy = new StringFilter{ StartsWith = VariationGroupingDetail_ProductFilterDTO.WarrantyPolicy };
            ProductFilter.ReturnPolicy = new StringFilter{ StartsWith = VariationGroupingDetail_ProductFilterDTO.ReturnPolicy };
            ProductFilter.ExpiredDate = new StringFilter{ StartsWith = VariationGroupingDetail_ProductFilterDTO.ExpiredDate };
            ProductFilter.ConditionOfUse = new StringFilter{ StartsWith = VariationGroupingDetail_ProductFilterDTO.ConditionOfUse };
            ProductFilter.MaximumPurchaseQuantity = new LongFilter{ Equal = VariationGroupingDetail_ProductFilterDTO.MaximumPurchaseQuantity };

            List<Product> Products = await ProductService.List(ProductFilter);
            List<VariationGroupingDetail_ProductDTO> VariationGroupingDetail_ProductDTOs = Products
                .Select(x => new VariationGroupingDetail_ProductDTO(x)).ToList();
            return VariationGroupingDetail_ProductDTOs;
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MVariationGrouping;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MProduct;


namespace WG.Controllers.variation_grouping.variation_grouping_master
{
    public class VariationGroupingMasterRoute : Root
    {
        public const string FE = "/variation-grouping/variation-grouping-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListProduct= Default + "/single-list-product";
    }

    public class VariationGroupingMasterController : ApiController
    {
        
        
        private IProductService ProductService;
        private IVariationGroupingService VariationGroupingService;

        public VariationGroupingMasterController(
            
            IProductService ProductService,
            IVariationGroupingService VariationGroupingService
        )
        {
            
            this.ProductService = ProductService;
            this.VariationGroupingService = VariationGroupingService;
        }


        [Route(VariationGroupingMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] VariationGroupingMaster_VariationGroupingFilterDTO VariationGroupingMaster_VariationGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGroupingFilter VariationGroupingFilter = ConvertFilterDTOToFilterEntity(VariationGroupingMaster_VariationGroupingFilterDTO);

            return await VariationGroupingService.Count(VariationGroupingFilter);
        }

        [Route(VariationGroupingMasterRoute.List), HttpPost]
        public async Task<List<VariationGroupingMaster_VariationGroupingDTO>> List([FromBody] VariationGroupingMaster_VariationGroupingFilterDTO VariationGroupingMaster_VariationGroupingFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGroupingFilter VariationGroupingFilter = ConvertFilterDTOToFilterEntity(VariationGroupingMaster_VariationGroupingFilterDTO);

            List<VariationGrouping> VariationGroupings = await VariationGroupingService.List(VariationGroupingFilter);

            return VariationGroupings.Select(c => new VariationGroupingMaster_VariationGroupingDTO(c)).ToList();
        }

        [Route(VariationGroupingMasterRoute.Get), HttpPost]
        public async Task<VariationGroupingMaster_VariationGroupingDTO> Get([FromBody]VariationGroupingMaster_VariationGroupingDTO VariationGroupingMaster_VariationGroupingDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            VariationGrouping VariationGrouping = await VariationGroupingService.Get(VariationGroupingMaster_VariationGroupingDTO.Id);
            return new VariationGroupingMaster_VariationGroupingDTO(VariationGrouping);
        }


        public VariationGroupingFilter ConvertFilterDTOToFilterEntity(VariationGroupingMaster_VariationGroupingFilterDTO VariationGroupingMaster_VariationGroupingFilterDTO)
        {
            VariationGroupingFilter VariationGroupingFilter = new VariationGroupingFilter();
            VariationGroupingFilter.Selects = VariationGroupingSelect.ALL;
            
            VariationGroupingFilter.Id = new LongFilter{ Equal = VariationGroupingMaster_VariationGroupingFilterDTO.Id };
            VariationGroupingFilter.Name = new StringFilter{ StartsWith = VariationGroupingMaster_VariationGroupingFilterDTO.Name };
            VariationGroupingFilter.ProductId = new LongFilter{ Equal = VariationGroupingMaster_VariationGroupingFilterDTO.ProductId };
            return VariationGroupingFilter;
        }
        
        
        [Route(VariationGroupingMasterRoute.SingleListProduct), HttpPost]
        public async Task<List<VariationGroupingMaster_ProductDTO>> SingleListProduct([FromBody] VariationGroupingMaster_ProductFilterDTO VariationGroupingMaster_ProductFilterDTO)
        {
            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            
            ProductFilter.Id = new LongFilter{ Equal = VariationGroupingMaster_ProductFilterDTO.Id };
            ProductFilter.Code = new StringFilter{ StartsWith = VariationGroupingMaster_ProductFilterDTO.Code };
            ProductFilter.Name = new StringFilter{ StartsWith = VariationGroupingMaster_ProductFilterDTO.Name };
            ProductFilter.Description = new StringFilter{ StartsWith = VariationGroupingMaster_ProductFilterDTO.Description };
            ProductFilter.TypeId = new LongFilter{ Equal = VariationGroupingMaster_ProductFilterDTO.TypeId };
            ProductFilter.StatusId = new LongFilter{ Equal = VariationGroupingMaster_ProductFilterDTO.StatusId };
            ProductFilter.MerchantId = new LongFilter{ Equal = VariationGroupingMaster_ProductFilterDTO.MerchantId };
            ProductFilter.CategoryId = new LongFilter{ Equal = VariationGroupingMaster_ProductFilterDTO.CategoryId };
            ProductFilter.BrandId = new LongFilter{ Equal = VariationGroupingMaster_ProductFilterDTO.BrandId };
            ProductFilter.WarrantyPolicy = new StringFilter{ StartsWith = VariationGroupingMaster_ProductFilterDTO.WarrantyPolicy };
            ProductFilter.ReturnPolicy = new StringFilter{ StartsWith = VariationGroupingMaster_ProductFilterDTO.ReturnPolicy };
            ProductFilter.ExpiredDate = new StringFilter{ StartsWith = VariationGroupingMaster_ProductFilterDTO.ExpiredDate };
            ProductFilter.ConditionOfUse = new StringFilter{ StartsWith = VariationGroupingMaster_ProductFilterDTO.ConditionOfUse };
            ProductFilter.MaximumPurchaseQuantity = new LongFilter{ Equal = VariationGroupingMaster_ProductFilterDTO.MaximumPurchaseQuantity };

            List<Product> Products = await ProductService.List(ProductFilter);
            List<VariationGroupingMaster_ProductDTO> VariationGroupingMaster_ProductDTOs = Products
                .Select(x => new VariationGroupingMaster_ProductDTO(x)).ToList();
            return VariationGroupingMaster_ProductDTOs;
        }

    }
}

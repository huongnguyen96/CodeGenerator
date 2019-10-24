

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MProduct;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MBrand;
using WG.Services.MCategory;
using WG.Services.MMerchant;
using WG.Services.MProductStatus;
using WG.Services.MProductType;


namespace WG.Controllers.product.product_master
{
    public class ProductMasterRoute : Root
    {
        public const string FE = "/product/product-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListBrand= Default + "/single-list-brand";
        public const string SingleListCategory= Default + "/single-list-category";
        public const string SingleListMerchant= Default + "/single-list-merchant";
        public const string SingleListProductStatus= Default + "/single-list-product-status";
        public const string SingleListProductType= Default + "/single-list-product-type";
    }

    public class ProductMasterController : ApiController
    {
        
        
        private IBrandService BrandService;
        private ICategoryService CategoryService;
        private IMerchantService MerchantService;
        private IProductStatusService ProductStatusService;
        private IProductTypeService ProductTypeService;
        private IProductService ProductService;

        public ProductMasterController(
            
            IBrandService BrandService,
            ICategoryService CategoryService,
            IMerchantService MerchantService,
            IProductStatusService ProductStatusService,
            IProductTypeService ProductTypeService,
            IProductService ProductService
        )
        {
            
            this.BrandService = BrandService;
            this.CategoryService = CategoryService;
            this.MerchantService = MerchantService;
            this.ProductStatusService = ProductStatusService;
            this.ProductTypeService = ProductTypeService;
            this.ProductService = ProductService;
        }


        [Route(ProductMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ProductMaster_ProductFilterDTO ProductMaster_ProductFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductFilter ProductFilter = ConvertFilterDTOToFilterEntity(ProductMaster_ProductFilterDTO);

            return await ProductService.Count(ProductFilter);
        }

        [Route(ProductMasterRoute.List), HttpPost]
        public async Task<List<ProductMaster_ProductDTO>> List([FromBody] ProductMaster_ProductFilterDTO ProductMaster_ProductFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductFilter ProductFilter = ConvertFilterDTOToFilterEntity(ProductMaster_ProductFilterDTO);

            List<Product> Products = await ProductService.List(ProductFilter);

            return Products.Select(c => new ProductMaster_ProductDTO(c)).ToList();
        }

        [Route(ProductMasterRoute.Get), HttpPost]
        public async Task<ProductMaster_ProductDTO> Get([FromBody]ProductMaster_ProductDTO ProductMaster_ProductDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Product Product = await ProductService.Get(ProductMaster_ProductDTO.Id);
            return new ProductMaster_ProductDTO(Product);
        }


        public ProductFilter ConvertFilterDTOToFilterEntity(ProductMaster_ProductFilterDTO ProductMaster_ProductFilterDTO)
        {
            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Selects = ProductSelect.ALL;
            
            ProductFilter.Id = new LongFilter{ Equal = ProductMaster_ProductFilterDTO.Id };
            ProductFilter.Code = new StringFilter{ StartsWith = ProductMaster_ProductFilterDTO.Code };
            ProductFilter.Name = new StringFilter{ StartsWith = ProductMaster_ProductFilterDTO.Name };
            ProductFilter.Description = new StringFilter{ StartsWith = ProductMaster_ProductFilterDTO.Description };
            ProductFilter.TypeId = new LongFilter{ Equal = ProductMaster_ProductFilterDTO.TypeId };
            ProductFilter.StatusId = new LongFilter{ Equal = ProductMaster_ProductFilterDTO.StatusId };
            ProductFilter.MerchantId = new LongFilter{ Equal = ProductMaster_ProductFilterDTO.MerchantId };
            ProductFilter.CategoryId = new LongFilter{ Equal = ProductMaster_ProductFilterDTO.CategoryId };
            ProductFilter.BrandId = new LongFilter{ Equal = ProductMaster_ProductFilterDTO.BrandId };
            ProductFilter.WarrantyPolicy = new StringFilter{ StartsWith = ProductMaster_ProductFilterDTO.WarrantyPolicy };
            ProductFilter.ReturnPolicy = new StringFilter{ StartsWith = ProductMaster_ProductFilterDTO.ReturnPolicy };
            ProductFilter.ExpiredDate = new StringFilter{ StartsWith = ProductMaster_ProductFilterDTO.ExpiredDate };
            ProductFilter.ConditionOfUse = new StringFilter{ StartsWith = ProductMaster_ProductFilterDTO.ConditionOfUse };
            ProductFilter.MaximumPurchaseQuantity = new LongFilter{ Equal = ProductMaster_ProductFilterDTO.MaximumPurchaseQuantity };
            return ProductFilter;
        }
        
        
        [Route(ProductMasterRoute.SingleListBrand), HttpPost]
        public async Task<List<ProductMaster_BrandDTO>> SingleListBrand([FromBody] ProductMaster_BrandFilterDTO ProductMaster_BrandFilterDTO)
        {
            BrandFilter BrandFilter = new BrandFilter();
            BrandFilter.Skip = 0;
            BrandFilter.Take = 20;
            BrandFilter.OrderBy = BrandOrder.Id;
            BrandFilter.OrderType = OrderType.ASC;
            BrandFilter.Selects = BrandSelect.ALL;
            
            BrandFilter.Id = new LongFilter{ Equal = ProductMaster_BrandFilterDTO.Id };
            BrandFilter.Name = new StringFilter{ StartsWith = ProductMaster_BrandFilterDTO.Name };
            BrandFilter.CategoryId = new LongFilter{ Equal = ProductMaster_BrandFilterDTO.CategoryId };

            List<Brand> Brands = await BrandService.List(BrandFilter);
            List<ProductMaster_BrandDTO> ProductMaster_BrandDTOs = Brands
                .Select(x => new ProductMaster_BrandDTO(x)).ToList();
            return ProductMaster_BrandDTOs;
        }

        [Route(ProductMasterRoute.SingleListCategory), HttpPost]
        public async Task<List<ProductMaster_CategoryDTO>> SingleListCategory([FromBody] ProductMaster_CategoryFilterDTO ProductMaster_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = ProductMaster_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = ProductMaster_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = ProductMaster_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = ProductMaster_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = ProductMaster_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<ProductMaster_CategoryDTO> ProductMaster_CategoryDTOs = Categorys
                .Select(x => new ProductMaster_CategoryDTO(x)).ToList();
            return ProductMaster_CategoryDTOs;
        }

        [Route(ProductMasterRoute.SingleListMerchant), HttpPost]
        public async Task<List<ProductMaster_MerchantDTO>> SingleListMerchant([FromBody] ProductMaster_MerchantFilterDTO ProductMaster_MerchantFilterDTO)
        {
            MerchantFilter MerchantFilter = new MerchantFilter();
            MerchantFilter.Skip = 0;
            MerchantFilter.Take = 20;
            MerchantFilter.OrderBy = MerchantOrder.Id;
            MerchantFilter.OrderType = OrderType.ASC;
            MerchantFilter.Selects = MerchantSelect.ALL;
            
            MerchantFilter.Id = new LongFilter{ Equal = ProductMaster_MerchantFilterDTO.Id };
            MerchantFilter.Name = new StringFilter{ StartsWith = ProductMaster_MerchantFilterDTO.Name };
            MerchantFilter.Phone = new StringFilter{ StartsWith = ProductMaster_MerchantFilterDTO.Phone };
            MerchantFilter.ContactPerson = new StringFilter{ StartsWith = ProductMaster_MerchantFilterDTO.ContactPerson };
            MerchantFilter.Address = new StringFilter{ StartsWith = ProductMaster_MerchantFilterDTO.Address };

            List<Merchant> Merchants = await MerchantService.List(MerchantFilter);
            List<ProductMaster_MerchantDTO> ProductMaster_MerchantDTOs = Merchants
                .Select(x => new ProductMaster_MerchantDTO(x)).ToList();
            return ProductMaster_MerchantDTOs;
        }

        [Route(ProductMasterRoute.SingleListProductStatus), HttpPost]
        public async Task<List<ProductMaster_ProductStatusDTO>> SingleListProductStatus([FromBody] ProductMaster_ProductStatusFilterDTO ProductMaster_ProductStatusFilterDTO)
        {
            ProductStatusFilter ProductStatusFilter = new ProductStatusFilter();
            ProductStatusFilter.Skip = 0;
            ProductStatusFilter.Take = 20;
            ProductStatusFilter.OrderBy = ProductStatusOrder.Id;
            ProductStatusFilter.OrderType = OrderType.ASC;
            ProductStatusFilter.Selects = ProductStatusSelect.ALL;
            
            ProductStatusFilter.Id = new LongFilter{ Equal = ProductMaster_ProductStatusFilterDTO.Id };
            ProductStatusFilter.Code = new StringFilter{ StartsWith = ProductMaster_ProductStatusFilterDTO.Code };
            ProductStatusFilter.Name = new StringFilter{ StartsWith = ProductMaster_ProductStatusFilterDTO.Name };

            List<ProductStatus> ProductStatuss = await ProductStatusService.List(ProductStatusFilter);
            List<ProductMaster_ProductStatusDTO> ProductMaster_ProductStatusDTOs = ProductStatuss
                .Select(x => new ProductMaster_ProductStatusDTO(x)).ToList();
            return ProductMaster_ProductStatusDTOs;
        }

        [Route(ProductMasterRoute.SingleListProductType), HttpPost]
        public async Task<List<ProductMaster_ProductTypeDTO>> SingleListProductType([FromBody] ProductMaster_ProductTypeFilterDTO ProductMaster_ProductTypeFilterDTO)
        {
            ProductTypeFilter ProductTypeFilter = new ProductTypeFilter();
            ProductTypeFilter.Skip = 0;
            ProductTypeFilter.Take = 20;
            ProductTypeFilter.OrderBy = ProductTypeOrder.Id;
            ProductTypeFilter.OrderType = OrderType.ASC;
            ProductTypeFilter.Selects = ProductTypeSelect.ALL;
            
            ProductTypeFilter.Id = new LongFilter{ Equal = ProductMaster_ProductTypeFilterDTO.Id };
            ProductTypeFilter.Code = new StringFilter{ StartsWith = ProductMaster_ProductTypeFilterDTO.Code };
            ProductTypeFilter.Name = new StringFilter{ StartsWith = ProductMaster_ProductTypeFilterDTO.Name };

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<ProductMaster_ProductTypeDTO> ProductMaster_ProductTypeDTOs = ProductTypes
                .Select(x => new ProductMaster_ProductTypeDTO(x)).ToList();
            return ProductMaster_ProductTypeDTOs;
        }

    }
}

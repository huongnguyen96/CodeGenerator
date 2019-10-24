

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


namespace WG.Controllers.product.product_detail
{
    public class ProductDetailRoute : Root
    {
        public const string FE = "/product/product-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListBrand= Default + "/single-list-brand";
        public const string SingleListCategory= Default + "/single-list-category";
        public const string SingleListMerchant= Default + "/single-list-merchant";
        public const string SingleListProductStatus= Default + "/single-list-product-status";
        public const string SingleListProductType= Default + "/single-list-product-type";
    }

    public class ProductDetailController : ApiController
    {
        
        
        private IBrandService BrandService;
        private ICategoryService CategoryService;
        private IMerchantService MerchantService;
        private IProductStatusService ProductStatusService;
        private IProductTypeService ProductTypeService;
        private IProductService ProductService;

        public ProductDetailController(
            
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


        [Route(ProductDetailRoute.Get), HttpPost]
        public async Task<ProductDetail_ProductDTO> Get([FromBody]ProductDetail_ProductDTO ProductDetail_ProductDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Product Product = await ProductService.Get(ProductDetail_ProductDTO.Id);
            return new ProductDetail_ProductDTO(Product);
        }


        [Route(ProductDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ProductDetail_ProductDTO>> Create([FromBody] ProductDetail_ProductDTO ProductDetail_ProductDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Product Product = ConvertDTOToEntity(ProductDetail_ProductDTO);

            Product = await ProductService.Create(Product);
            ProductDetail_ProductDTO = new ProductDetail_ProductDTO(Product);
            if (Product.IsValidated)
                return ProductDetail_ProductDTO;
            else
                return BadRequest(ProductDetail_ProductDTO);        
        }

        [Route(ProductDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ProductDetail_ProductDTO>> Update([FromBody] ProductDetail_ProductDTO ProductDetail_ProductDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Product Product = ConvertDTOToEntity(ProductDetail_ProductDTO);

            Product = await ProductService.Update(Product);
            ProductDetail_ProductDTO = new ProductDetail_ProductDTO(Product);
            if (Product.IsValidated)
                return ProductDetail_ProductDTO;
            else
                return BadRequest(ProductDetail_ProductDTO);        
        }

        [Route(ProductDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ProductDetail_ProductDTO>> Delete([FromBody] ProductDetail_ProductDTO ProductDetail_ProductDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Product Product = ConvertDTOToEntity(ProductDetail_ProductDTO);

            Product = await ProductService.Delete(Product);
            ProductDetail_ProductDTO = new ProductDetail_ProductDTO(Product);
            if (Product.IsValidated)
                return ProductDetail_ProductDTO;
            else
                return BadRequest(ProductDetail_ProductDTO);        
        }

        public Product ConvertDTOToEntity(ProductDetail_ProductDTO ProductDetail_ProductDTO)
        {
            Product Product = new Product();
            
            Product.Id = ProductDetail_ProductDTO.Id;
            Product.Code = ProductDetail_ProductDTO.Code;
            Product.Name = ProductDetail_ProductDTO.Name;
            Product.Description = ProductDetail_ProductDTO.Description;
            Product.TypeId = ProductDetail_ProductDTO.TypeId;
            Product.StatusId = ProductDetail_ProductDTO.StatusId;
            Product.MerchantId = ProductDetail_ProductDTO.MerchantId;
            Product.CategoryId = ProductDetail_ProductDTO.CategoryId;
            Product.BrandId = ProductDetail_ProductDTO.BrandId;
            Product.WarrantyPolicy = ProductDetail_ProductDTO.WarrantyPolicy;
            Product.ReturnPolicy = ProductDetail_ProductDTO.ReturnPolicy;
            Product.ExpiredDate = ProductDetail_ProductDTO.ExpiredDate;
            Product.ConditionOfUse = ProductDetail_ProductDTO.ConditionOfUse;
            Product.MaximumPurchaseQuantity = ProductDetail_ProductDTO.MaximumPurchaseQuantity;
            return Product;
        }
        
        
        [Route(ProductDetailRoute.SingleListBrand), HttpPost]
        public async Task<List<ProductDetail_BrandDTO>> SingleListBrand([FromBody] ProductDetail_BrandFilterDTO ProductDetail_BrandFilterDTO)
        {
            BrandFilter BrandFilter = new BrandFilter();
            BrandFilter.Skip = 0;
            BrandFilter.Take = 20;
            BrandFilter.OrderBy = BrandOrder.Id;
            BrandFilter.OrderType = OrderType.ASC;
            BrandFilter.Selects = BrandSelect.ALL;
            
            BrandFilter.Id = new LongFilter{ Equal = ProductDetail_BrandFilterDTO.Id };
            BrandFilter.Name = new StringFilter{ StartsWith = ProductDetail_BrandFilterDTO.Name };
            BrandFilter.CategoryId = new LongFilter{ Equal = ProductDetail_BrandFilterDTO.CategoryId };

            List<Brand> Brands = await BrandService.List(BrandFilter);
            List<ProductDetail_BrandDTO> ProductDetail_BrandDTOs = Brands
                .Select(x => new ProductDetail_BrandDTO(x)).ToList();
            return ProductDetail_BrandDTOs;
        }

        [Route(ProductDetailRoute.SingleListCategory), HttpPost]
        public async Task<List<ProductDetail_CategoryDTO>> SingleListCategory([FromBody] ProductDetail_CategoryFilterDTO ProductDetail_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = ProductDetail_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = ProductDetail_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = ProductDetail_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = ProductDetail_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = ProductDetail_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<ProductDetail_CategoryDTO> ProductDetail_CategoryDTOs = Categorys
                .Select(x => new ProductDetail_CategoryDTO(x)).ToList();
            return ProductDetail_CategoryDTOs;
        }

        [Route(ProductDetailRoute.SingleListMerchant), HttpPost]
        public async Task<List<ProductDetail_MerchantDTO>> SingleListMerchant([FromBody] ProductDetail_MerchantFilterDTO ProductDetail_MerchantFilterDTO)
        {
            MerchantFilter MerchantFilter = new MerchantFilter();
            MerchantFilter.Skip = 0;
            MerchantFilter.Take = 20;
            MerchantFilter.OrderBy = MerchantOrder.Id;
            MerchantFilter.OrderType = OrderType.ASC;
            MerchantFilter.Selects = MerchantSelect.ALL;
            
            MerchantFilter.Id = new LongFilter{ Equal = ProductDetail_MerchantFilterDTO.Id };
            MerchantFilter.Name = new StringFilter{ StartsWith = ProductDetail_MerchantFilterDTO.Name };
            MerchantFilter.Phone = new StringFilter{ StartsWith = ProductDetail_MerchantFilterDTO.Phone };
            MerchantFilter.ContactPerson = new StringFilter{ StartsWith = ProductDetail_MerchantFilterDTO.ContactPerson };
            MerchantFilter.Address = new StringFilter{ StartsWith = ProductDetail_MerchantFilterDTO.Address };

            List<Merchant> Merchants = await MerchantService.List(MerchantFilter);
            List<ProductDetail_MerchantDTO> ProductDetail_MerchantDTOs = Merchants
                .Select(x => new ProductDetail_MerchantDTO(x)).ToList();
            return ProductDetail_MerchantDTOs;
        }

        [Route(ProductDetailRoute.SingleListProductStatus), HttpPost]
        public async Task<List<ProductDetail_ProductStatusDTO>> SingleListProductStatus([FromBody] ProductDetail_ProductStatusFilterDTO ProductDetail_ProductStatusFilterDTO)
        {
            ProductStatusFilter ProductStatusFilter = new ProductStatusFilter();
            ProductStatusFilter.Skip = 0;
            ProductStatusFilter.Take = 20;
            ProductStatusFilter.OrderBy = ProductStatusOrder.Id;
            ProductStatusFilter.OrderType = OrderType.ASC;
            ProductStatusFilter.Selects = ProductStatusSelect.ALL;
            
            ProductStatusFilter.Id = new LongFilter{ Equal = ProductDetail_ProductStatusFilterDTO.Id };
            ProductStatusFilter.Code = new StringFilter{ StartsWith = ProductDetail_ProductStatusFilterDTO.Code };
            ProductStatusFilter.Name = new StringFilter{ StartsWith = ProductDetail_ProductStatusFilterDTO.Name };

            List<ProductStatus> ProductStatuss = await ProductStatusService.List(ProductStatusFilter);
            List<ProductDetail_ProductStatusDTO> ProductDetail_ProductStatusDTOs = ProductStatuss
                .Select(x => new ProductDetail_ProductStatusDTO(x)).ToList();
            return ProductDetail_ProductStatusDTOs;
        }

        [Route(ProductDetailRoute.SingleListProductType), HttpPost]
        public async Task<List<ProductDetail_ProductTypeDTO>> SingleListProductType([FromBody] ProductDetail_ProductTypeFilterDTO ProductDetail_ProductTypeFilterDTO)
        {
            ProductTypeFilter ProductTypeFilter = new ProductTypeFilter();
            ProductTypeFilter.Skip = 0;
            ProductTypeFilter.Take = 20;
            ProductTypeFilter.OrderBy = ProductTypeOrder.Id;
            ProductTypeFilter.OrderType = OrderType.ASC;
            ProductTypeFilter.Selects = ProductTypeSelect.ALL;
            
            ProductTypeFilter.Id = new LongFilter{ Equal = ProductDetail_ProductTypeFilterDTO.Id };
            ProductTypeFilter.Code = new StringFilter{ StartsWith = ProductDetail_ProductTypeFilterDTO.Code };
            ProductTypeFilter.Name = new StringFilter{ StartsWith = ProductDetail_ProductTypeFilterDTO.Name };

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<ProductDetail_ProductTypeDTO> ProductDetail_ProductTypeDTOs = ProductTypes
                .Select(x => new ProductDetail_ProductTypeDTO(x)).ToList();
            return ProductDetail_ProductTypeDTOs;
        }

    }
}

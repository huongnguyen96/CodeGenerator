

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MProductType;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.product_type.product_type_detail
{
    public class ProductTypeDetailRoute : Root
    {
        public const string FE = "/product-type/product-type-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ProductTypeDetailController : ApiController
    {
        
        
        private IProductTypeService ProductTypeService;

        public ProductTypeDetailController(
            
            IProductTypeService ProductTypeService
        )
        {
            
            this.ProductTypeService = ProductTypeService;
        }


        [Route(ProductTypeDetailRoute.Get), HttpPost]
        public async Task<ProductTypeDetail_ProductTypeDTO> Get([FromBody]ProductTypeDetail_ProductTypeDTO ProductTypeDetail_ProductTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductType ProductType = await ProductTypeService.Get(ProductTypeDetail_ProductTypeDTO.Id);
            return new ProductTypeDetail_ProductTypeDTO(ProductType);
        }


        [Route(ProductTypeDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ProductTypeDetail_ProductTypeDTO>> Create([FromBody] ProductTypeDetail_ProductTypeDTO ProductTypeDetail_ProductTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductType ProductType = ConvertDTOToEntity(ProductTypeDetail_ProductTypeDTO);

            ProductType = await ProductTypeService.Create(ProductType);
            ProductTypeDetail_ProductTypeDTO = new ProductTypeDetail_ProductTypeDTO(ProductType);
            if (ProductType.IsValidated)
                return ProductTypeDetail_ProductTypeDTO;
            else
                return BadRequest(ProductTypeDetail_ProductTypeDTO);        
        }

        [Route(ProductTypeDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ProductTypeDetail_ProductTypeDTO>> Update([FromBody] ProductTypeDetail_ProductTypeDTO ProductTypeDetail_ProductTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductType ProductType = ConvertDTOToEntity(ProductTypeDetail_ProductTypeDTO);

            ProductType = await ProductTypeService.Update(ProductType);
            ProductTypeDetail_ProductTypeDTO = new ProductTypeDetail_ProductTypeDTO(ProductType);
            if (ProductType.IsValidated)
                return ProductTypeDetail_ProductTypeDTO;
            else
                return BadRequest(ProductTypeDetail_ProductTypeDTO);        
        }

        [Route(ProductTypeDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ProductTypeDetail_ProductTypeDTO>> Delete([FromBody] ProductTypeDetail_ProductTypeDTO ProductTypeDetail_ProductTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductType ProductType = ConvertDTOToEntity(ProductTypeDetail_ProductTypeDTO);

            ProductType = await ProductTypeService.Delete(ProductType);
            ProductTypeDetail_ProductTypeDTO = new ProductTypeDetail_ProductTypeDTO(ProductType);
            if (ProductType.IsValidated)
                return ProductTypeDetail_ProductTypeDTO;
            else
                return BadRequest(ProductTypeDetail_ProductTypeDTO);        
        }

        public ProductType ConvertDTOToEntity(ProductTypeDetail_ProductTypeDTO ProductTypeDetail_ProductTypeDTO)
        {
            ProductType ProductType = new ProductType();
            
            ProductType.Id = ProductTypeDetail_ProductTypeDTO.Id;
            ProductType.Code = ProductTypeDetail_ProductTypeDTO.Code;
            ProductType.Name = ProductTypeDetail_ProductTypeDTO.Name;
            return ProductType;
        }
        
        
    }
}

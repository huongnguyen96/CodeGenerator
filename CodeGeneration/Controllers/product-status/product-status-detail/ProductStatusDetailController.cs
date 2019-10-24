

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MProductStatus;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.product_status.product_status_detail
{
    public class ProductStatusDetailRoute : Root
    {
        public const string FE = "/product-status/product-status-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
    }

    public class ProductStatusDetailController : ApiController
    {
        
        
        private IProductStatusService ProductStatusService;

        public ProductStatusDetailController(
            
            IProductStatusService ProductStatusService
        )
        {
            
            this.ProductStatusService = ProductStatusService;
        }


        [Route(ProductStatusDetailRoute.Get), HttpPost]
        public async Task<ProductStatusDetail_ProductStatusDTO> Get([FromBody]ProductStatusDetail_ProductStatusDTO ProductStatusDetail_ProductStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductStatus ProductStatus = await ProductStatusService.Get(ProductStatusDetail_ProductStatusDTO.Id);
            return new ProductStatusDetail_ProductStatusDTO(ProductStatus);
        }


        [Route(ProductStatusDetailRoute.Create), HttpPost]
        public async Task<ActionResult<ProductStatusDetail_ProductStatusDTO>> Create([FromBody] ProductStatusDetail_ProductStatusDTO ProductStatusDetail_ProductStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductStatus ProductStatus = ConvertDTOToEntity(ProductStatusDetail_ProductStatusDTO);

            ProductStatus = await ProductStatusService.Create(ProductStatus);
            ProductStatusDetail_ProductStatusDTO = new ProductStatusDetail_ProductStatusDTO(ProductStatus);
            if (ProductStatus.IsValidated)
                return ProductStatusDetail_ProductStatusDTO;
            else
                return BadRequest(ProductStatusDetail_ProductStatusDTO);        
        }

        [Route(ProductStatusDetailRoute.Update), HttpPost]
        public async Task<ActionResult<ProductStatusDetail_ProductStatusDTO>> Update([FromBody] ProductStatusDetail_ProductStatusDTO ProductStatusDetail_ProductStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductStatus ProductStatus = ConvertDTOToEntity(ProductStatusDetail_ProductStatusDTO);

            ProductStatus = await ProductStatusService.Update(ProductStatus);
            ProductStatusDetail_ProductStatusDTO = new ProductStatusDetail_ProductStatusDTO(ProductStatus);
            if (ProductStatus.IsValidated)
                return ProductStatusDetail_ProductStatusDTO;
            else
                return BadRequest(ProductStatusDetail_ProductStatusDTO);        
        }

        [Route(ProductStatusDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<ProductStatusDetail_ProductStatusDTO>> Delete([FromBody] ProductStatusDetail_ProductStatusDTO ProductStatusDetail_ProductStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductStatus ProductStatus = ConvertDTOToEntity(ProductStatusDetail_ProductStatusDTO);

            ProductStatus = await ProductStatusService.Delete(ProductStatus);
            ProductStatusDetail_ProductStatusDTO = new ProductStatusDetail_ProductStatusDTO(ProductStatus);
            if (ProductStatus.IsValidated)
                return ProductStatusDetail_ProductStatusDTO;
            else
                return BadRequest(ProductStatusDetail_ProductStatusDTO);        
        }

        public ProductStatus ConvertDTOToEntity(ProductStatusDetail_ProductStatusDTO ProductStatusDetail_ProductStatusDTO)
        {
            ProductStatus ProductStatus = new ProductStatus();
            
            ProductStatus.Id = ProductStatusDetail_ProductStatusDTO.Id;
            ProductStatus.Code = ProductStatusDetail_ProductStatusDTO.Code;
            ProductStatus.Name = ProductStatusDetail_ProductStatusDTO.Name;
            return ProductStatus;
        }
        
        
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MProductStatus;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.product_status.product_status_master
{
    public class ProductStatusMasterRoute : Root
    {
        public const string FE = "/product-status/product-status-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class ProductStatusMasterController : ApiController
    {
        
        
        private IProductStatusService ProductStatusService;

        public ProductStatusMasterController(
            
            IProductStatusService ProductStatusService
        )
        {
            
            this.ProductStatusService = ProductStatusService;
        }


        [Route(ProductStatusMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ProductStatusMaster_ProductStatusFilterDTO ProductStatusMaster_ProductStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductStatusFilter ProductStatusFilter = ConvertFilterDTOToFilterEntity(ProductStatusMaster_ProductStatusFilterDTO);

            return await ProductStatusService.Count(ProductStatusFilter);
        }

        [Route(ProductStatusMasterRoute.List), HttpPost]
        public async Task<List<ProductStatusMaster_ProductStatusDTO>> List([FromBody] ProductStatusMaster_ProductStatusFilterDTO ProductStatusMaster_ProductStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductStatusFilter ProductStatusFilter = ConvertFilterDTOToFilterEntity(ProductStatusMaster_ProductStatusFilterDTO);

            List<ProductStatus> ProductStatuss = await ProductStatusService.List(ProductStatusFilter);

            return ProductStatuss.Select(c => new ProductStatusMaster_ProductStatusDTO(c)).ToList();
        }

        [Route(ProductStatusMasterRoute.Get), HttpPost]
        public async Task<ProductStatusMaster_ProductStatusDTO> Get([FromBody]ProductStatusMaster_ProductStatusDTO ProductStatusMaster_ProductStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductStatus ProductStatus = await ProductStatusService.Get(ProductStatusMaster_ProductStatusDTO.Id);
            return new ProductStatusMaster_ProductStatusDTO(ProductStatus);
        }


        public ProductStatusFilter ConvertFilterDTOToFilterEntity(ProductStatusMaster_ProductStatusFilterDTO ProductStatusMaster_ProductStatusFilterDTO)
        {
            ProductStatusFilter ProductStatusFilter = new ProductStatusFilter();
            ProductStatusFilter.Selects = ProductStatusSelect.ALL;
            
            ProductStatusFilter.Id = new LongFilter{ Equal = ProductStatusMaster_ProductStatusFilterDTO.Id };
            ProductStatusFilter.Code = new StringFilter{ StartsWith = ProductStatusMaster_ProductStatusFilterDTO.Code };
            ProductStatusFilter.Name = new StringFilter{ StartsWith = ProductStatusMaster_ProductStatusFilterDTO.Name };
            return ProductStatusFilter;
        }
        
        
    }
}

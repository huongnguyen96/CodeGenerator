

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MProductType;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.product_type.product_type_master
{
    public class ProductTypeMasterRoute : Root
    {
        public const string FE = "/product-type/product-type-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
    }

    public class ProductTypeMasterController : ApiController
    {
        
        
        private IProductTypeService ProductTypeService;

        public ProductTypeMasterController(
            
            IProductTypeService ProductTypeService
        )
        {
            
            this.ProductTypeService = ProductTypeService;
        }


        [Route(ProductTypeMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] ProductTypeMaster_ProductTypeFilterDTO ProductTypeMaster_ProductTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductTypeFilter ProductTypeFilter = ConvertFilterDTOToFilterEntity(ProductTypeMaster_ProductTypeFilterDTO);

            return await ProductTypeService.Count(ProductTypeFilter);
        }

        [Route(ProductTypeMasterRoute.List), HttpPost]
        public async Task<List<ProductTypeMaster_ProductTypeDTO>> List([FromBody] ProductTypeMaster_ProductTypeFilterDTO ProductTypeMaster_ProductTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductTypeFilter ProductTypeFilter = ConvertFilterDTOToFilterEntity(ProductTypeMaster_ProductTypeFilterDTO);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);

            return ProductTypes.Select(c => new ProductTypeMaster_ProductTypeDTO(c)).ToList();
        }

        [Route(ProductTypeMasterRoute.Get), HttpPost]
        public async Task<ProductTypeMaster_ProductTypeDTO> Get([FromBody]ProductTypeMaster_ProductTypeDTO ProductTypeMaster_ProductTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            ProductType ProductType = await ProductTypeService.Get(ProductTypeMaster_ProductTypeDTO.Id);
            return new ProductTypeMaster_ProductTypeDTO(ProductType);
        }


        public ProductTypeFilter ConvertFilterDTOToFilterEntity(ProductTypeMaster_ProductTypeFilterDTO ProductTypeMaster_ProductTypeFilterDTO)
        {
            ProductTypeFilter ProductTypeFilter = new ProductTypeFilter();
            ProductTypeFilter.Selects = ProductTypeSelect.ALL;
            
            ProductTypeFilter.Id = new LongFilter{ Equal = ProductTypeMaster_ProductTypeFilterDTO.Id };
            ProductTypeFilter.Code = new StringFilter{ StartsWith = ProductTypeMaster_ProductTypeFilterDTO.Code };
            ProductTypeFilter.Name = new StringFilter{ StartsWith = ProductTypeMaster_ProductTypeFilterDTO.Name };
            return ProductTypeFilter;
        }
        
        
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MBrand;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MCategory;


namespace WG.Controllers.brand.brand_master
{
    public class BrandMasterRoute : Root
    {
        public const string FE = "/brand/brand-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListCategory= Default + "/single-list-category";
    }

    public class BrandMasterController : ApiController
    {
        
        
        private ICategoryService CategoryService;
        private IBrandService BrandService;

        public BrandMasterController(
            
            ICategoryService CategoryService,
            IBrandService BrandService
        )
        {
            
            this.CategoryService = CategoryService;
            this.BrandService = BrandService;
        }


        [Route(BrandMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] BrandMaster_BrandFilterDTO BrandMaster_BrandFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            BrandFilter BrandFilter = ConvertFilterDTOToFilterEntity(BrandMaster_BrandFilterDTO);

            return await BrandService.Count(BrandFilter);
        }

        [Route(BrandMasterRoute.List), HttpPost]
        public async Task<List<BrandMaster_BrandDTO>> List([FromBody] BrandMaster_BrandFilterDTO BrandMaster_BrandFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            BrandFilter BrandFilter = ConvertFilterDTOToFilterEntity(BrandMaster_BrandFilterDTO);

            List<Brand> Brands = await BrandService.List(BrandFilter);

            return Brands.Select(c => new BrandMaster_BrandDTO(c)).ToList();
        }

        [Route(BrandMasterRoute.Get), HttpPost]
        public async Task<BrandMaster_BrandDTO> Get([FromBody]BrandMaster_BrandDTO BrandMaster_BrandDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Brand Brand = await BrandService.Get(BrandMaster_BrandDTO.Id);
            return new BrandMaster_BrandDTO(Brand);
        }


        public BrandFilter ConvertFilterDTOToFilterEntity(BrandMaster_BrandFilterDTO BrandMaster_BrandFilterDTO)
        {
            BrandFilter BrandFilter = new BrandFilter();
            BrandFilter.Selects = BrandSelect.ALL;
            
            BrandFilter.Id = new LongFilter{ Equal = BrandMaster_BrandFilterDTO.Id };
            BrandFilter.Name = new StringFilter{ StartsWith = BrandMaster_BrandFilterDTO.Name };
            BrandFilter.CategoryId = new LongFilter{ Equal = BrandMaster_BrandFilterDTO.CategoryId };
            return BrandFilter;
        }
        
        
        [Route(BrandMasterRoute.SingleListCategory), HttpPost]
        public async Task<List<BrandMaster_CategoryDTO>> SingleListCategory([FromBody] BrandMaster_CategoryFilterDTO BrandMaster_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = BrandMaster_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = BrandMaster_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = BrandMaster_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = BrandMaster_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = BrandMaster_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<BrandMaster_CategoryDTO> BrandMaster_CategoryDTOs = Categorys
                .Select(x => new BrandMaster_CategoryDTO(x)).ToList();
            return BrandMaster_CategoryDTOs;
        }

    }
}

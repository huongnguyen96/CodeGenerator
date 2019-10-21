

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MBrand;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MCategory;


namespace WG.Controllers.brand.brand_detail
{
    public class BrandDetailRoute : Root
    {
        public const string FE = "/brand/brand-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListCategory="/single-list-category";
    }

    public class BrandDetailController : ApiController
    {
        
        
        private ICategoryService CategoryService;
        private IBrandService BrandService;

        public BrandDetailController(
            
            ICategoryService CategoryService,
            IBrandService BrandService
        )
        {
            
            this.CategoryService = CategoryService;
            this.BrandService = BrandService;
        }


        [Route(BrandDetailRoute.Get), HttpPost]
        public async Task<BrandDetail_BrandDTO> Get([FromBody]BrandDetail_BrandDTO BrandDetail_BrandDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Brand Brand = await BrandService.Get(BrandDetail_BrandDTO.Id);
            return new BrandDetail_BrandDTO(Brand);
        }


        [Route(BrandDetailRoute.Create), HttpPost]
        public async Task<ActionResult<BrandDetail_BrandDTO>> Create([FromBody] BrandDetail_BrandDTO BrandDetail_BrandDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Brand Brand = ConvertDTOToEntity(BrandDetail_BrandDTO);

            Brand = await BrandService.Create(Brand);
            BrandDetail_BrandDTO = new BrandDetail_BrandDTO(Brand);
            if (Brand.IsValidated)
                return BrandDetail_BrandDTO;
            else
                return BadRequest(BrandDetail_BrandDTO);        
        }

        [Route(BrandDetailRoute.Update), HttpPost]
        public async Task<ActionResult<BrandDetail_BrandDTO>> Update([FromBody] BrandDetail_BrandDTO BrandDetail_BrandDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Brand Brand = ConvertDTOToEntity(BrandDetail_BrandDTO);

            Brand = await BrandService.Update(Brand);
            BrandDetail_BrandDTO = new BrandDetail_BrandDTO(Brand);
            if (Brand.IsValidated)
                return BrandDetail_BrandDTO;
            else
                return BadRequest(BrandDetail_BrandDTO);        
        }

        [Route(BrandDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<BrandDetail_BrandDTO>> Delete([FromBody] BrandDetail_BrandDTO BrandDetail_BrandDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Brand Brand = ConvertDTOToEntity(BrandDetail_BrandDTO);

            Brand = await BrandService.Delete(Brand);
            BrandDetail_BrandDTO = new BrandDetail_BrandDTO(Brand);
            if (Brand.IsValidated)
                return BrandDetail_BrandDTO;
            else
                return BadRequest(BrandDetail_BrandDTO);        
        }

        public Brand ConvertDTOToEntity(BrandDetail_BrandDTO BrandDetail_BrandDTO)
        {
            Brand Brand = new Brand();
            
            Brand.Id = BrandDetail_BrandDTO.Id;
            Brand.Name = BrandDetail_BrandDTO.Name;
            Brand.CategoryId = BrandDetail_BrandDTO.CategoryId;
            return Brand;
        }
        
        
        [Route(BrandDetailRoute.SingleListCategory), HttpPost]
        public async Task<List<BrandDetail_CategoryDTO>> SingleListCategory([FromBody] BrandDetail_CategoryFilterDTO BrandDetail_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = BrandDetail_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = BrandDetail_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = BrandDetail_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = BrandDetail_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = BrandDetail_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<BrandDetail_CategoryDTO> BrandDetail_CategoryDTOs = Categorys
                .Select(x => new BrandDetail_CategoryDTO(x)).ToList();
            return BrandDetail_CategoryDTOs;
        }

    }
}

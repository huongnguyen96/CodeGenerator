

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MCategory;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MCategory;


namespace WG.Controllers.category.category_master
{
    public class CategoryMasterRoute : Root
    {
        public const string FE = "/category/category-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
        public const string SingleListCategory="/single-list-category";
    }

    public class CategoryMasterController : ApiController
    {
        
        
        private ICategoryService CategoryService;

        public CategoryMasterController(
            
            ICategoryService CategoryService
        )
        {
            
            this.CategoryService = CategoryService;
        }


        [Route(CategoryMasterRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] CategoryMaster_CategoryFilterDTO CategoryMaster_CategoryFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CategoryFilter CategoryFilter = ConvertFilterDTOToFilterEntity(CategoryMaster_CategoryFilterDTO);

            return await CategoryService.Count(CategoryFilter);
        }

        [Route(CategoryMasterRoute.List), HttpPost]
        public async Task<List<CategoryMaster_CategoryDTO>> List([FromBody] CategoryMaster_CategoryFilterDTO CategoryMaster_CategoryFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CategoryFilter CategoryFilter = ConvertFilterDTOToFilterEntity(CategoryMaster_CategoryFilterDTO);

            List<Category> Categorys = await CategoryService.List(CategoryFilter);

            return Categorys.Select(c => new CategoryMaster_CategoryDTO(c)).ToList();
        }

        [Route(CategoryMasterRoute.Get), HttpPost]
        public async Task<CategoryMaster_CategoryDTO> Get([FromBody]CategoryMaster_CategoryDTO CategoryMaster_CategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Category Category = await CategoryService.Get(CategoryMaster_CategoryDTO.Id);
            return new CategoryMaster_CategoryDTO(Category);
        }


        public CategoryFilter ConvertFilterDTOToFilterEntity(CategoryMaster_CategoryFilterDTO CategoryMaster_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            
            CategoryFilter.Id = new LongFilter{ Equal = CategoryMaster_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = CategoryMaster_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = CategoryMaster_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = CategoryMaster_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = CategoryMaster_CategoryFilterDTO.Icon };
            return CategoryFilter;
        }
        
        
        [Route(CategoryMasterRoute.SingleListCategory), HttpPost]
        public async Task<List<CategoryMaster_CategoryDTO>> SingleListCategory([FromBody] CategoryMaster_CategoryFilterDTO CategoryMaster_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = CategoryMaster_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = CategoryMaster_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = CategoryMaster_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = CategoryMaster_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = CategoryMaster_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<CategoryMaster_CategoryDTO> CategoryMaster_CategoryDTOs = Categorys
                .Select(x => new CategoryMaster_CategoryDTO(x)).ToList();
            return CategoryMaster_CategoryDTOs;
        }

    }
}

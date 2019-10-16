

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MCategory;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;



namespace WG.Controllers.category.category_master
{
    public class CategoryMasterRoute : Root
    {
        public const string FE = "/category/category-master";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        
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
            return CategoryFilter;
        }
        
        
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WeGift.Services.MCategory;
using Microsoft.AspNetCore.Mvc;
using WeGift.Entities;

namespace WeGift.Controllers.category.category_list
{
    public class CategoryListRoute : Root
    {
        public const string FE = "category/category-list";
        private const string Default = Base + FE;
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
    }

    public class CategoryListController : ApiController
    {
        private ICategoryService CategoryService;

        public CategoryListController(
            ICategoryService CategoryService
        )
        {
            this.CategoryService = CategoryService;
        }

        [Route(CategoryListRoute.Count), HttpPost]
        public async Task<int> Count([FromBody] CategoryList_CategoryFilterDTO CategoryList_CategoryFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CategoryFilter CategoryFilter = ConvertFilterDTOtoFilterEntity(CategoryList_CategoryFilterDTO);

            return await CategoryService.Count(CategoryFilter);
        }

        [Route(CategoryListRoute.List), HttpPost]
        public async Task<List<CategoryList_CategoryDTO>> List([FromBody] CategoryList_CategoryFilterDTO CategoryList_CategoryFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            CategoryFilter CategoryFilter = ConvertFilterDTOtoFilterEntity(CategoryList_CategoryFilterDTO);

            List<Category> Categorys = await CategoryService.List(CategoryFilter);

            return Categorys.Select(c => new CategoryList_CategoryDTO(c)).ToList();
        }

        [Route(CategoryListRoute.Get), HttpPost]
        public async Task<CategoryList_CategoryDTO> Get([FromBody]CategoryList_CategoryDTO CategoryList_CategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Category Category = await CategoryService.Get(CategoryList_CategoryDTO.Id);
            return new CategoryList_CategoryDTO(Category);
        }


        public CategoryFilter ConvertFilterDTOtoFilterEntity(CategoryList_CategoryFilterDTO CategoryList_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            
            CategoryFilter.Id = CategoryList_CategoryFilterDTO.Id;
            CategoryFilter.Code = CategoryList_CategoryFilterDTO.Code;
            CategoryFilter.Name = CategoryList_CategoryFilterDTO.Name;
            return CategoryFilter;
        }
    }
}

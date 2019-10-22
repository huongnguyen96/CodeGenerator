

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WG.Services.MCategory;
using Microsoft.AspNetCore.Mvc;
using WG.Entities;

using WG.Services.MCategory;


namespace WG.Controllers.category.category_detail
{
    public class CategoryDetailRoute : Root
    {
        public const string FE = "/category/category-detail";
        private const string Default = Base + FE;
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        
        public const string SingleListCategory= Default + "/single-list-category";
    }

    public class CategoryDetailController : ApiController
    {
        
        
        private ICategoryService CategoryService;

        public CategoryDetailController(
            
            ICategoryService CategoryService
        )
        {
            
            this.CategoryService = CategoryService;
        }


        [Route(CategoryDetailRoute.Get), HttpPost]
        public async Task<CategoryDetail_CategoryDTO> Get([FromBody]CategoryDetail_CategoryDTO CategoryDetail_CategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Category Category = await CategoryService.Get(CategoryDetail_CategoryDTO.Id);
            return new CategoryDetail_CategoryDTO(Category);
        }


        [Route(CategoryDetailRoute.Create), HttpPost]
        public async Task<ActionResult<CategoryDetail_CategoryDTO>> Create([FromBody] CategoryDetail_CategoryDTO CategoryDetail_CategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Category Category = ConvertDTOToEntity(CategoryDetail_CategoryDTO);

            Category = await CategoryService.Create(Category);
            CategoryDetail_CategoryDTO = new CategoryDetail_CategoryDTO(Category);
            if (Category.IsValidated)
                return CategoryDetail_CategoryDTO;
            else
                return BadRequest(CategoryDetail_CategoryDTO);        
        }

        [Route(CategoryDetailRoute.Update), HttpPost]
        public async Task<ActionResult<CategoryDetail_CategoryDTO>> Update([FromBody] CategoryDetail_CategoryDTO CategoryDetail_CategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Category Category = ConvertDTOToEntity(CategoryDetail_CategoryDTO);

            Category = await CategoryService.Update(Category);
            CategoryDetail_CategoryDTO = new CategoryDetail_CategoryDTO(Category);
            if (Category.IsValidated)
                return CategoryDetail_CategoryDTO;
            else
                return BadRequest(CategoryDetail_CategoryDTO);        
        }

        [Route(CategoryDetailRoute.Delete), HttpPost]
        public async Task<ActionResult<CategoryDetail_CategoryDTO>> Delete([FromBody] CategoryDetail_CategoryDTO CategoryDetail_CategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            Category Category = ConvertDTOToEntity(CategoryDetail_CategoryDTO);

            Category = await CategoryService.Delete(Category);
            CategoryDetail_CategoryDTO = new CategoryDetail_CategoryDTO(Category);
            if (Category.IsValidated)
                return CategoryDetail_CategoryDTO;
            else
                return BadRequest(CategoryDetail_CategoryDTO);        
        }

        public Category ConvertDTOToEntity(CategoryDetail_CategoryDTO CategoryDetail_CategoryDTO)
        {
            Category Category = new Category();
            
            Category.Id = CategoryDetail_CategoryDTO.Id;
            Category.Code = CategoryDetail_CategoryDTO.Code;
            Category.Name = CategoryDetail_CategoryDTO.Name;
            Category.ParentId = CategoryDetail_CategoryDTO.ParentId;
            Category.Icon = CategoryDetail_CategoryDTO.Icon;
            return Category;
        }
        
        
        [Route(CategoryDetailRoute.SingleListCategory), HttpPost]
        public async Task<List<CategoryDetail_CategoryDTO>> SingleListCategory([FromBody] CategoryDetail_CategoryFilterDTO CategoryDetail_CategoryFilterDTO)
        {
            CategoryFilter CategoryFilter = new CategoryFilter();
            CategoryFilter.Skip = 0;
            CategoryFilter.Take = 20;
            CategoryFilter.OrderBy = CategoryOrder.Id;
            CategoryFilter.OrderType = OrderType.ASC;
            CategoryFilter.Selects = CategorySelect.ALL;
            
            CategoryFilter.Id = new LongFilter{ Equal = CategoryDetail_CategoryFilterDTO.Id };
            CategoryFilter.Code = new StringFilter{ StartsWith = CategoryDetail_CategoryFilterDTO.Code };
            CategoryFilter.Name = new StringFilter{ StartsWith = CategoryDetail_CategoryFilterDTO.Name };
            CategoryFilter.ParentId = new LongFilter{ Equal = CategoryDetail_CategoryFilterDTO.ParentId };
            CategoryFilter.Icon = new StringFilter{ StartsWith = CategoryDetail_CategoryFilterDTO.Icon };

            List<Category> Categorys = await CategoryService.List(CategoryFilter);
            List<CategoryDetail_CategoryDTO> CategoryDetail_CategoryDTOs = Categorys
                .Select(x => new CategoryDetail_CategoryDTO(x)).ToList();
            return CategoryDetail_CategoryDTOs;
        }

    }
}

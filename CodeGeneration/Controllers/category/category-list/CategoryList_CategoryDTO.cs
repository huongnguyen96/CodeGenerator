
using WeGift.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace WeGift.Controllers.category.categoryList
{
    public class CategoryList_CategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CategoryList_CategoryDTO() {}
        public CategoryList_CategoryDTO(Category Category)
        {
            
            this.Id = Category.Id;
            this.Code = Category.Code;
            this.Name = Category.Name;
        }
    }

    public class CategoryList_CategoryFilterDTO : FilterDTO
    {
    }
}

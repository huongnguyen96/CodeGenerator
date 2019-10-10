
using WeGift.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace WeGift.Controllers.category.category_list
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
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.brand.brand_detail
{
    public class BrandDetail_CategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
        public BrandDetail_CategoryDTO() {}
        public BrandDetail_CategoryDTO(Category Category)
        {
            
            this.Id = Category.Id;
            this.Code = Category.Code;
            this.Name = Category.Name;
            this.ParentId = Category.ParentId;
            this.Icon = Category.Icon;
        }
    }

    public class BrandDetail_CategoryFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
        public CategoryOrder OrderBy { get; set; }
    }
}

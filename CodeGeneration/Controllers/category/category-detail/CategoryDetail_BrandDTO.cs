
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.category.category_detail
{
    public class CategoryDetail_BrandDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public CategoryDetail_BrandDTO() {}
        public CategoryDetail_BrandDTO(Brand Brand)
        {
            
            this.Id = Brand.Id;
            this.Name = Brand.Name;
            this.CategoryId = Brand.CategoryId;
        }
    }

    public class CategoryDetail_BrandFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? CategoryId { get; set; }
        public BrandOrder OrderBy { get; set; }
    }
}

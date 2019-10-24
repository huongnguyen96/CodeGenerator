
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.category.category_detail
{
    public class CategoryDetail_ProductTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CategoryDetail_ProductTypeDTO() {}
        public CategoryDetail_ProductTypeDTO(ProductType ProductType)
        {
            
            this.Id = ProductType.Id;
            this.Code = ProductType.Code;
            this.Name = ProductType.Name;
        }
    }

    public class CategoryDetail_ProductTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductTypeOrder OrderBy { get; set; }
    }
}

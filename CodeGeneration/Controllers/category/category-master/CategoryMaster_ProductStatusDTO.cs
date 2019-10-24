
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.category.category_master
{
    public class CategoryMaster_ProductStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CategoryMaster_ProductStatusDTO() {}
        public CategoryMaster_ProductStatusDTO(ProductStatus ProductStatus)
        {
            
            this.Id = ProductStatus.Id;
            this.Code = ProductStatus.Code;
            this.Name = ProductStatus.Name;
        }
    }

    public class CategoryMaster_ProductStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductStatusOrder OrderBy { get; set; }
    }
}

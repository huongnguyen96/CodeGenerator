
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product_type.product_type_master
{
    public class ProductTypeMaster_BrandDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public ProductTypeMaster_BrandDTO() {}
        public ProductTypeMaster_BrandDTO(Brand Brand)
        {
            
            this.Id = Brand.Id;
            this.Name = Brand.Name;
            this.CategoryId = Brand.CategoryId;
        }
    }

    public class ProductTypeMaster_BrandFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? CategoryId { get; set; }
        public BrandOrder OrderBy { get; set; }
    }
}

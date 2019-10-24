
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product.product_master
{
    public class ProductMaster_BrandDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public ProductMaster_BrandDTO() {}
        public ProductMaster_BrandDTO(Brand Brand)
        {
            
            this.Id = Brand.Id;
            this.Name = Brand.Name;
            this.CategoryId = Brand.CategoryId;
        }
    }

    public class ProductMaster_BrandFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? CategoryId { get; set; }
        public BrandOrder OrderBy { get; set; }
    }
}

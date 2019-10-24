
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product.product_detail
{
    public class ProductDetail_BrandDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public ProductDetail_BrandDTO() {}
        public ProductDetail_BrandDTO(Brand Brand)
        {
            
            this.Id = Brand.Id;
            this.Name = Brand.Name;
            this.CategoryId = Brand.CategoryId;
        }
    }

    public class ProductDetail_BrandFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? CategoryId { get; set; }
        public BrandOrder OrderBy { get; set; }
    }
}

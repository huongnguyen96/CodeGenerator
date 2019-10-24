
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.brand.brand_detail
{
    public class BrandDetail_ProductTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public BrandDetail_ProductTypeDTO() {}
        public BrandDetail_ProductTypeDTO(ProductType ProductType)
        {
            
            this.Id = ProductType.Id;
            this.Code = ProductType.Code;
            this.Name = ProductType.Name;
        }
    }

    public class BrandDetail_ProductTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductTypeOrder OrderBy { get; set; }
    }
}

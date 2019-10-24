
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product_status.product_status_detail
{
    public class ProductStatusDetail_ProductTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductStatusDetail_ProductTypeDTO() {}
        public ProductStatusDetail_ProductTypeDTO(ProductType ProductType)
        {
            
            this.Id = ProductType.Id;
            this.Code = ProductType.Code;
            this.Name = ProductType.Name;
        }
    }

    public class ProductStatusDetail_ProductTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductTypeOrder OrderBy { get; set; }
    }
}

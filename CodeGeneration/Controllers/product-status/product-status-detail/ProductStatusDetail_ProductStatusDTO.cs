
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product_status.product_status_detail
{
    public class ProductStatusDetail_ProductStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductStatusDetail_ProductStatusDTO() {}
        public ProductStatusDetail_ProductStatusDTO(ProductStatus ProductStatus)
        {
            
            this.Id = ProductStatus.Id;
            this.Code = ProductStatus.Code;
            this.Name = ProductStatus.Name;
        }
    }

    public class ProductStatusDetail_ProductStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductStatusOrder OrderBy { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product_type.product_type_detail
{
    public class ProductTypeDetail_ProductStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductTypeDetail_ProductStatusDTO() {}
        public ProductTypeDetail_ProductStatusDTO(ProductStatus ProductStatus)
        {
            
            this.Id = ProductStatus.Id;
            this.Code = ProductStatus.Code;
            this.Name = ProductStatus.Name;
        }
    }

    public class ProductTypeDetail_ProductStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductStatusOrder OrderBy { get; set; }
    }
}

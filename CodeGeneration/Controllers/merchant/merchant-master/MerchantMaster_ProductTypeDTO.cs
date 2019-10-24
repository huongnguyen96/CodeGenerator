
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.merchant.merchant_master
{
    public class MerchantMaster_ProductTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public MerchantMaster_ProductTypeDTO() {}
        public MerchantMaster_ProductTypeDTO(ProductType ProductType)
        {
            
            this.Id = ProductType.Id;
            this.Code = ProductType.Code;
            this.Name = ProductType.Name;
        }
    }

    public class MerchantMaster_ProductTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductTypeOrder OrderBy { get; set; }
    }
}

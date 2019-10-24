
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.merchant.merchant_master
{
    public class MerchantMaster_ProductStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public MerchantMaster_ProductStatusDTO() {}
        public MerchantMaster_ProductStatusDTO(ProductStatus ProductStatus)
        {
            
            this.Id = ProductStatus.Id;
            this.Code = ProductStatus.Code;
            this.Name = ProductStatus.Name;
        }
    }

    public class MerchantMaster_ProductStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProductStatusOrder OrderBy { get; set; }
    }
}

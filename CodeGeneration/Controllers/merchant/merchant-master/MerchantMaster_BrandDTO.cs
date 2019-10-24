
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.merchant.merchant_master
{
    public class MerchantMaster_BrandDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public MerchantMaster_BrandDTO() {}
        public MerchantMaster_BrandDTO(Brand Brand)
        {
            
            this.Id = Brand.Id;
            this.Name = Brand.Name;
            this.CategoryId = Brand.CategoryId;
        }
    }

    public class MerchantMaster_BrandFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? CategoryId { get; set; }
        public BrandOrder OrderBy { get; set; }
    }
}

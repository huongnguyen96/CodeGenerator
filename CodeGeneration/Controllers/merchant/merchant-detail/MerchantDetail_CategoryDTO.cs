
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.merchant.merchant_detail
{
    public class MerchantDetail_CategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
        public MerchantDetail_CategoryDTO() {}
        public MerchantDetail_CategoryDTO(Category Category)
        {
            
            this.Id = Category.Id;
            this.Code = Category.Code;
            this.Name = Category.Name;
            this.ParentId = Category.ParentId;
            this.Icon = Category.Icon;
        }
    }

    public class MerchantDetail_CategoryFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
        public CategoryOrder OrderBy { get; set; }
    }
}

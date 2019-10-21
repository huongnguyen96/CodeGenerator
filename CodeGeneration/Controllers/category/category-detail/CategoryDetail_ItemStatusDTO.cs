
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.category.category_detail
{
    public class CategoryDetail_ItemStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CategoryDetail_ItemStatusDTO() {}
        public CategoryDetail_ItemStatusDTO(ItemStatus ItemStatus)
        {
            
            this.Id = ItemStatus.Id;
            this.Code = ItemStatus.Code;
            this.Name = ItemStatus.Name;
        }
    }

    public class CategoryDetail_ItemStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

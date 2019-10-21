
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.category.category_detail
{
    public class CategoryDetail_ItemTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CategoryDetail_ItemTypeDTO() {}
        public CategoryDetail_ItemTypeDTO(ItemType ItemType)
        {
            
            this.Id = ItemType.Id;
            this.Code = ItemType.Code;
            this.Name = ItemType.Name;
        }
    }

    public class CategoryDetail_ItemTypeFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

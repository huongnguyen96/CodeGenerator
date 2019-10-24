
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product_type.product_type_master
{
    public class ProductTypeMaster_CategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
        public ProductTypeMaster_CategoryDTO() {}
        public ProductTypeMaster_CategoryDTO(Category Category)
        {
            
            this.Id = Category.Id;
            this.Code = Category.Code;
            this.Name = Category.Name;
            this.ParentId = Category.ParentId;
            this.Icon = Category.Icon;
        }
    }

    public class ProductTypeMaster_CategoryFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Icon { get; set; }
        public CategoryOrder OrderBy { get; set; }
    }
}

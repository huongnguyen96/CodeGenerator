
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_type.item_type_detail
{
    public class ItemTypeDetail_ItemUnitOfMeasureDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemTypeDetail_ItemUnitOfMeasureDTO() {}
        public ItemTypeDetail_ItemUnitOfMeasureDTO(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            
            this.Id = ItemUnitOfMeasure.Id;
            this.Code = ItemUnitOfMeasure.Code;
            this.Name = ItemUnitOfMeasure.Name;
        }
    }

    public class ItemTypeDetail_ItemUnitOfMeasureFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

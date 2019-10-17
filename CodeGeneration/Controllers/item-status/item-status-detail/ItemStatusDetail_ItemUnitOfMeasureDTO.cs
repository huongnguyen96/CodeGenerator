
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_status.item_status_detail
{
    public class ItemStatusDetail_ItemUnitOfMeasureDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemStatusDetail_ItemUnitOfMeasureDTO() {}
        public ItemStatusDetail_ItemUnitOfMeasureDTO(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            
            this.Id = ItemUnitOfMeasure.Id;
            this.Code = ItemUnitOfMeasure.Code;
            this.Name = ItemUnitOfMeasure.Name;
        }
    }

    public class ItemStatusDetail_ItemUnitOfMeasureFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

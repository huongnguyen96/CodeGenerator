
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_unit_of_measure.item_unit_of_measure_master
{
    public class ItemUnitOfMeasureMaster_ItemStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemUnitOfMeasureMaster_ItemStatusDTO() {}
        public ItemUnitOfMeasureMaster_ItemStatusDTO(ItemStatus ItemStatus)
        {
            
            this.Id = ItemStatus.Id;
            this.Code = ItemStatus.Code;
            this.Name = ItemStatus.Name;
        }
    }

    public class ItemUnitOfMeasureMaster_ItemStatusFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

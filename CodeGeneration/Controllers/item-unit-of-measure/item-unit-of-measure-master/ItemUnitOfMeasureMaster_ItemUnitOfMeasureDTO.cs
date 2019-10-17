
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item_unit_of_measure.item_unit_of_measure_master
{
    public class ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO() {}
        public ItemUnitOfMeasureMaster_ItemUnitOfMeasureDTO(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            
            this.Id = ItemUnitOfMeasure.Id;
            this.Code = ItemUnitOfMeasure.Code;
            this.Name = ItemUnitOfMeasure.Name;
        }
    }

    public class ItemUnitOfMeasureMaster_ItemUnitOfMeasureFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

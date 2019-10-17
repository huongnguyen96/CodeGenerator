
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_detail
{
    public class WarehouseDetail_ItemUnitOfMeasureDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public WarehouseDetail_ItemUnitOfMeasureDTO() {}
        public WarehouseDetail_ItemUnitOfMeasureDTO(ItemUnitOfMeasure ItemUnitOfMeasure)
        {
            
            this.Id = ItemUnitOfMeasure.Id;
            this.Code = ItemUnitOfMeasure.Code;
            this.Name = ItemUnitOfMeasure.Name;
        }
    }

    public class WarehouseDetail_ItemUnitOfMeasureFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}

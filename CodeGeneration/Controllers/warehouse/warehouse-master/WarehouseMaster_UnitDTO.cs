
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.warehouse.warehouse_master
{
    public class WarehouseMaster_UnitDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public long? ThirdVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public WarehouseMaster_UnitDTO() {}
        public WarehouseMaster_UnitDTO(Unit Unit)
        {
            
            this.Id = Unit.Id;
            this.FirstVariationId = Unit.FirstVariationId;
            this.SecondVariationId = Unit.SecondVariationId;
            this.ThirdVariationId = Unit.ThirdVariationId;
            this.SKU = Unit.SKU;
            this.Price = Unit.Price;
        }
    }

    public class WarehouseMaster_UnitFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public long? ThirdVariationId { get; set; }
        public string SKU { get; set; }
        public long? Price { get; set; }
    }
}

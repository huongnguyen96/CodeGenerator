
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.unit.unit_detail
{
    public class UnitDetail_UnitDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public long? ThirdVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public UnitDetail_UnitDTO() {}
        public UnitDetail_UnitDTO(Unit Unit)
        {
            
            this.Id = Unit.Id;
            this.FirstVariationId = Unit.FirstVariationId;
            this.SecondVariationId = Unit.SecondVariationId;
            this.ThirdVariationId = Unit.ThirdVariationId;
            this.SKU = Unit.SKU;
            this.Price = Unit.Price;
        }
    }

    public class UnitDetail_UnitFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public long? ThirdVariationId { get; set; }
        public string SKU { get; set; }
        public long? Price { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.unit.unit_detail
{
    public class UnitDetail_VariationDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }
        public UnitDetail_VariationDTO() {}
        public UnitDetail_VariationDTO(Variation Variation)
        {
            
            this.Id = Variation.Id;
            this.Name = Variation.Name;
            this.VariationGroupingId = Variation.VariationGroupingId;
        }
    }

    public class UnitDetail_VariationFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? VariationGroupingId { get; set; }
    }
}

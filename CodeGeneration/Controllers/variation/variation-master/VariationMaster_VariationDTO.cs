
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.variation.variation_master
{
    public class VariationMaster_VariationDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }
        public VariationMaster_VariationDTO() {}
        public VariationMaster_VariationDTO(Variation Variation)
        {
            
            this.Id = Variation.Id;
            this.Name = Variation.Name;
            this.VariationGroupingId = Variation.VariationGroupingId;
        }
    }

    public class VariationMaster_VariationFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? VariationGroupingId { get; set; }
        public VariationOrder OrderBy { get; set; }
    }
}

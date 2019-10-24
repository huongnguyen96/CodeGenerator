
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.variation_grouping.variation_grouping_master
{
    public class VariationGroupingMaster_VariationDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }
        public VariationGroupingMaster_VariationDTO() {}
        public VariationGroupingMaster_VariationDTO(Variation Variation)
        {
            
            this.Id = Variation.Id;
            this.Name = Variation.Name;
            this.VariationGroupingId = Variation.VariationGroupingId;
        }
    }

    public class VariationGroupingMaster_VariationFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? VariationGroupingId { get; set; }
        public VariationOrder OrderBy { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.variation_grouping.variation_grouping_detail
{
    public class VariationGroupingDetail_VariationDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }
        public VariationGroupingDetail_VariationDTO() {}
        public VariationGroupingDetail_VariationDTO(Variation Variation)
        {
            
            this.Id = Variation.Id;
            this.Name = Variation.Name;
            this.VariationGroupingId = Variation.VariationGroupingId;
        }
    }

    public class VariationGroupingDetail_VariationFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? VariationGroupingId { get; set; }
        public VariationOrder OrderBy { get; set; }
    }
}

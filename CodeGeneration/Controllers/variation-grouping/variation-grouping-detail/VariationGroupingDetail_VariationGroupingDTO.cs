
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.variation_grouping.variation_grouping_detail
{
    public class VariationGroupingDetail_VariationGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductId { get; set; }
        public VariationGroupingDetail_VariationGroupingDTO() {}
        public VariationGroupingDetail_VariationGroupingDTO(VariationGrouping VariationGrouping)
        {
            
            this.Id = VariationGrouping.Id;
            this.Name = VariationGrouping.Name;
            this.ProductId = VariationGrouping.ProductId;
        }
    }

    public class VariationGroupingDetail_VariationGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? ProductId { get; set; }
        public VariationGroupingOrder OrderBy { get; set; }
    }
}

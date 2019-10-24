
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.variation.variation_master
{
    public class VariationMaster_VariationGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductId { get; set; }
        public VariationMaster_VariationGroupingDTO() {}
        public VariationMaster_VariationGroupingDTO(VariationGrouping VariationGrouping)
        {
            
            this.Id = VariationGrouping.Id;
            this.Name = VariationGrouping.Name;
            this.ProductId = VariationGrouping.ProductId;
        }
    }

    public class VariationMaster_VariationGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? ProductId { get; set; }
        public VariationGroupingOrder OrderBy { get; set; }
    }
}

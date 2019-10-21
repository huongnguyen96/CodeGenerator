
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.variation_grouping.variation_grouping_master
{
    public class VariationGroupingMaster_VariationGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long ItemId { get; set; }
        public VariationGroupingMaster_VariationGroupingDTO() {}
        public VariationGroupingMaster_VariationGroupingDTO(VariationGrouping VariationGrouping)
        {
            
            this.Id = VariationGrouping.Id;
            this.Name = VariationGrouping.Name;
            this.ItemId = VariationGrouping.ItemId;
        }
    }

    public class VariationGroupingMaster_VariationGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? ItemId { get; set; }
    }
}

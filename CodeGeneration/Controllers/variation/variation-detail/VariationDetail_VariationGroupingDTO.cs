
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.variation.variation_detail
{
    public class VariationDetail_VariationGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductId { get; set; }
        public VariationDetail_VariationGroupingDTO() {}
        public VariationDetail_VariationGroupingDTO(VariationGrouping VariationGrouping)
        {
            
            this.Id = VariationGrouping.Id;
            this.Name = VariationGrouping.Name;
            this.ProductId = VariationGrouping.ProductId;
        }
    }

    public class VariationDetail_VariationGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? ProductId { get; set; }
        public VariationGroupingOrder OrderBy { get; set; }
    }
}

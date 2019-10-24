
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product.product_master
{
    public class ProductMaster_VariationGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductId { get; set; }
        public ProductMaster_VariationGroupingDTO() {}
        public ProductMaster_VariationGroupingDTO(VariationGrouping VariationGrouping)
        {
            
            this.Id = VariationGrouping.Id;
            this.Name = VariationGrouping.Name;
            this.ProductId = VariationGrouping.ProductId;
        }
    }

    public class ProductMaster_VariationGroupingFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? ProductId { get; set; }
        public VariationGroupingOrder OrderBy { get; set; }
    }
}

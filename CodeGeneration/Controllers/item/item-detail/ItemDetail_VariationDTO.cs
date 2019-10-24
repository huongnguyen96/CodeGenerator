
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_detail
{
    public class ItemDetail_VariationDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public long VariationGroupingId { get; set; }
        public ItemDetail_VariationDTO() {}
        public ItemDetail_VariationDTO(Variation Variation)
        {
            
            this.Id = Variation.Id;
            this.Name = Variation.Name;
            this.VariationGroupingId = Variation.VariationGroupingId;
        }
    }

    public class ItemDetail_VariationFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? VariationGroupingId { get; set; }
        public VariationOrder OrderBy { get; set; }
    }
}

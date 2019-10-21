
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount.discount_master
{
    public class DiscountMaster_DiscountItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public DiscountMaster_UnitDTO Unit { get; set; }
        public DiscountMaster_DiscountItemDTO() {}
        public DiscountMaster_DiscountItemDTO(DiscountItem DiscountItem)
        {
            
            this.Id = DiscountItem.Id;
            this.UnitId = DiscountItem.UnitId;
            this.DiscountValue = DiscountItem.DiscountValue;
            this.DiscountId = DiscountItem.DiscountId;
            this.Unit = new DiscountMaster_UnitDTO(DiscountItem.Unit);

        }
    }

    public class DiscountMaster_DiscountItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
    }
}

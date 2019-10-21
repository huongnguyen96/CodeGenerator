
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.unit.unit_master
{
    public class UnitMaster_DiscountItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public UnitMaster_DiscountDTO Discount { get; set; }
        public UnitMaster_DiscountItemDTO() {}
        public UnitMaster_DiscountItemDTO(DiscountItem DiscountItem)
        {
            
            this.Id = DiscountItem.Id;
            this.UnitId = DiscountItem.UnitId;
            this.DiscountValue = DiscountItem.DiscountValue;
            this.DiscountId = DiscountItem.DiscountId;
            this.Discount = new UnitMaster_DiscountDTO(DiscountItem.Discount);

        }
    }

    public class UnitMaster_DiscountItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
    }
}

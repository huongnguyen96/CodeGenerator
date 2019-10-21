
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount.discount_detail
{
    public class DiscountDetail_DiscountItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public DiscountDetail_UnitDTO Unit { get; set; }
        public DiscountDetail_DiscountItemDTO() {}
        public DiscountDetail_DiscountItemDTO(DiscountItem DiscountItem)
        {
            
            this.Id = DiscountItem.Id;
            this.UnitId = DiscountItem.UnitId;
            this.DiscountValue = DiscountItem.DiscountValue;
            this.DiscountId = DiscountItem.DiscountId;
            this.Unit = new DiscountDetail_UnitDTO(DiscountItem.Unit);

        }
    }

    public class DiscountDetail_DiscountItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
    }
}

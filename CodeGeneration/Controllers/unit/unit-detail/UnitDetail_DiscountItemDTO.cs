
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.unit.unit_detail
{
    public class UnitDetail_DiscountItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public UnitDetail_DiscountDTO Discount { get; set; }
        public UnitDetail_DiscountItemDTO() {}
        public UnitDetail_DiscountItemDTO(DiscountItem DiscountItem)
        {
            
            this.Id = DiscountItem.Id;
            this.UnitId = DiscountItem.UnitId;
            this.DiscountValue = DiscountItem.DiscountValue;
            this.DiscountId = DiscountItem.DiscountId;
            this.Discount = new UnitDetail_DiscountDTO(DiscountItem.Discount);

        }
    }

    public class UnitDetail_DiscountItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
    }
}

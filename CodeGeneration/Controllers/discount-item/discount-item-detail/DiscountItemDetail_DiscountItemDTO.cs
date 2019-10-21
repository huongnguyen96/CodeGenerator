
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount_item.discount_item_detail
{
    public class DiscountItemDetail_DiscountItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public DiscountItemDetail_DiscountDTO Discount { get; set; }
        public DiscountItemDetail_UnitDTO Unit { get; set; }
        public DiscountItemDetail_DiscountItemDTO() {}
        public DiscountItemDetail_DiscountItemDTO(DiscountItem DiscountItem)
        {
            
            this.Id = DiscountItem.Id;
            this.UnitId = DiscountItem.UnitId;
            this.DiscountValue = DiscountItem.DiscountValue;
            this.DiscountId = DiscountItem.DiscountId;
            this.Discount = new DiscountItemDetail_DiscountDTO(DiscountItem.Discount);

            this.Unit = new DiscountItemDetail_UnitDTO(DiscountItem.Unit);

        }
    }

    public class DiscountItemDetail_DiscountItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
    }
}

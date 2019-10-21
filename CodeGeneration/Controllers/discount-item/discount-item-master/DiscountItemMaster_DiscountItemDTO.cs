
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount_item.discount_item_master
{
    public class DiscountItemMaster_DiscountItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public DiscountItemMaster_DiscountDTO Discount { get; set; }
        public DiscountItemMaster_UnitDTO Unit { get; set; }
        public DiscountItemMaster_DiscountItemDTO() {}
        public DiscountItemMaster_DiscountItemDTO(DiscountItem DiscountItem)
        {
            
            this.Id = DiscountItem.Id;
            this.UnitId = DiscountItem.UnitId;
            this.DiscountValue = DiscountItem.DiscountValue;
            this.DiscountId = DiscountItem.DiscountId;
            this.Discount = new DiscountItemMaster_DiscountDTO(DiscountItem.Discount);

            this.Unit = new DiscountItemMaster_UnitDTO(DiscountItem.Unit);

        }
    }

    public class DiscountItemMaster_DiscountItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? UnitId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
    }
}

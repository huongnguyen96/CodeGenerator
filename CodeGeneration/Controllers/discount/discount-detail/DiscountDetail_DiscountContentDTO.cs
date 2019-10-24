
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount.discount_detail
{
    public class DiscountDetail_DiscountContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public DiscountDetail_ItemDTO Item { get; set; }
        public DiscountDetail_DiscountContentDTO() {}
        public DiscountDetail_DiscountContentDTO(DiscountContent DiscountContent)
        {
            
            this.Id = DiscountContent.Id;
            this.ItemId = DiscountContent.ItemId;
            this.DiscountValue = DiscountContent.DiscountValue;
            this.DiscountId = DiscountContent.DiscountId;
            this.Item = new DiscountDetail_ItemDTO(DiscountContent.Item);

        }
    }

    public class DiscountDetail_DiscountContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
        public DiscountContentOrder OrderBy { get; set; }
    }
}

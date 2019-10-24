
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount_content.discount_content_detail
{
    public class DiscountContentDetail_DiscountContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public DiscountContentDetail_DiscountDTO Discount { get; set; }
        public DiscountContentDetail_ItemDTO Item { get; set; }
        public DiscountContentDetail_DiscountContentDTO() {}
        public DiscountContentDetail_DiscountContentDTO(DiscountContent DiscountContent)
        {
            
            this.Id = DiscountContent.Id;
            this.ItemId = DiscountContent.ItemId;
            this.DiscountValue = DiscountContent.DiscountValue;
            this.DiscountId = DiscountContent.DiscountId;
            this.Discount = new DiscountContentDetail_DiscountDTO(DiscountContent.Discount);

            this.Item = new DiscountContentDetail_ItemDTO(DiscountContent.Item);

        }
    }

    public class DiscountContentDetail_DiscountContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
        public DiscountContentOrder OrderBy { get; set; }
    }
}

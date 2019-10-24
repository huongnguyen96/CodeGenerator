
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount_content.discount_content_master
{
    public class DiscountContentMaster_DiscountContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public DiscountContentMaster_DiscountDTO Discount { get; set; }
        public DiscountContentMaster_ItemDTO Item { get; set; }
        public DiscountContentMaster_DiscountContentDTO() {}
        public DiscountContentMaster_DiscountContentDTO(DiscountContent DiscountContent)
        {
            
            this.Id = DiscountContent.Id;
            this.ItemId = DiscountContent.ItemId;
            this.DiscountValue = DiscountContent.DiscountValue;
            this.DiscountId = DiscountContent.DiscountId;
            this.Discount = new DiscountContentMaster_DiscountDTO(DiscountContent.Discount);

            this.Item = new DiscountContentMaster_ItemDTO(DiscountContent.Item);

        }
    }

    public class DiscountContentMaster_DiscountContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
        public DiscountContentOrder OrderBy { get; set; }
    }
}

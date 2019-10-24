
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_master
{
    public class ItemMaster_DiscountContentDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long DiscountValue { get; set; }
        public long DiscountId { get; set; }
        public ItemMaster_DiscountDTO Discount { get; set; }
        public ItemMaster_DiscountContentDTO() {}
        public ItemMaster_DiscountContentDTO(DiscountContent DiscountContent)
        {
            
            this.Id = DiscountContent.Id;
            this.ItemId = DiscountContent.ItemId;
            this.DiscountValue = DiscountContent.DiscountValue;
            this.DiscountId = DiscountContent.DiscountId;
            this.Discount = new ItemMaster_DiscountDTO(DiscountContent.Discount);

        }
    }

    public class ItemMaster_DiscountContentFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ItemId { get; set; }
        public long? DiscountValue { get; set; }
        public long? DiscountId { get; set; }
        public DiscountContentOrder OrderBy { get; set; }
    }
}


using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_detail
{
    public class ItemDetail_DiscountDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
        public ItemDetail_DiscountDTO() {}
        public ItemDetail_DiscountDTO(Discount Discount)
        {
            
            this.Id = Discount.Id;
            this.Name = Discount.Name;
            this.Start = Discount.Start;
            this.End = Discount.End;
            this.Type = Discount.Type;
        }
    }

    public class ItemDetail_DiscountFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Type { get; set; }
        public DiscountOrder OrderBy { get; set; }
    }
}

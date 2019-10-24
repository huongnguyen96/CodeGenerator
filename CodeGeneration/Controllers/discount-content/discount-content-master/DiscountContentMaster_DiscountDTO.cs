
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.discount_content.discount_content_master
{
    public class DiscountContentMaster_DiscountDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
        public DiscountContentMaster_DiscountDTO() {}
        public DiscountContentMaster_DiscountDTO(Discount Discount)
        {
            
            this.Id = Discount.Id;
            this.Name = Discount.Name;
            this.Start = Discount.Start;
            this.End = Discount.End;
            this.Type = Discount.Type;
        }
    }

    public class DiscountContentMaster_DiscountFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Type { get; set; }
        public DiscountOrder OrderBy { get; set; }
    }
}

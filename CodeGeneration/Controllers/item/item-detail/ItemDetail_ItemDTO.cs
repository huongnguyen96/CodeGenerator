
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.item.item_detail
{
    public class ItemDetail_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemDetail_ItemDTO() {}
        public ItemDetail_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.Code = Item.Code;
            this.Name = Item.Name;
        }
    }

    public class ItemDetail_ItemFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
    }
}

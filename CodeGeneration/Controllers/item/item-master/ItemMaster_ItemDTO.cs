
using WeGift.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeGift.Controllers.item.item_master
{
    public class ItemMaster_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemMaster_ItemDTO() {}
        public ItemMaster_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.Code = Item.Code;
            this.Name = Item.Name;
        }
    }

    public class ItemMaster_ItemFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
    }
}
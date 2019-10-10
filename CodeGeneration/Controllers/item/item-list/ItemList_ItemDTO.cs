
using WeGift.Entities;
using Common;
using System;
using System.Collections.Generic;

namespace WeGift.Controllers.item.item_list
{
    public class ItemList_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemList_ItemDTO() {}
        public ItemList_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.Code = Item.Code;
            this.Name = Item.Name;
        }
    }

    public class ItemList_ItemFilterDTO : FilterDTO
    {
        
        public LongFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
    }
}

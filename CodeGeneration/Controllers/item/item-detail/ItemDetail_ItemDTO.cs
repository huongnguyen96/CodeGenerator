
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.item.item_detail
{
    public class ItemDetail_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public long MinPrice { get; set; }
        public ItemDetail_ItemDTO() {}
        public ItemDetail_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.ProductId = Item.ProductId;
            this.FirstVariationId = Item.FirstVariationId;
            this.SecondVariationId = Item.SecondVariationId;
            this.SKU = Item.SKU;
            this.Price = Item.Price;
            this.MinPrice = Item.MinPrice;
        }
    }

    public class ItemDetail_ItemFilterDTO : FilterDTO
    {
        
        public long? Id { get; set; }
        public long? ProductId { get; set; }
        public long? FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public string SKU { get; set; }
        public long? Price { get; set; }
        public long? MinPrice { get; set; }
        public ItemOrder OrderBy { get; set; }
    }
}

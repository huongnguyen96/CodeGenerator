
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product.product_master
{
    public class ProductMaster_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public long MinPrice { get; set; }
        public ProductMaster_VariationDTO FirstVariation { get; set; }
        public ProductMaster_VariationDTO SecondVariation { get; set; }
        public ProductMaster_ItemDTO() {}
        public ProductMaster_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.ProductId = Item.ProductId;
            this.FirstVariationId = Item.FirstVariationId;
            this.SecondVariationId = Item.SecondVariationId;
            this.SKU = Item.SKU;
            this.Price = Item.Price;
            this.MinPrice = Item.MinPrice;
            this.FirstVariation = new ProductMaster_VariationDTO(Item.FirstVariation);

            this.SecondVariation = new ProductMaster_VariationDTO(Item.SecondVariation);

        }
    }

    public class ProductMaster_ItemFilterDTO : FilterDTO
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

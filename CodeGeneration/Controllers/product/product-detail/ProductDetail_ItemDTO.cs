
using WG.Entities;
using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WG.Controllers.product.product_detail
{
    public class ProductDetail_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public long MinPrice { get; set; }
        public ProductDetail_VariationDTO FirstVariation { get; set; }
        public ProductDetail_VariationDTO SecondVariation { get; set; }
        public ProductDetail_ItemDTO() {}
        public ProductDetail_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            this.ProductId = Item.ProductId;
            this.FirstVariationId = Item.FirstVariationId;
            this.SecondVariationId = Item.SecondVariationId;
            this.SKU = Item.SKU;
            this.Price = Item.Price;
            this.MinPrice = Item.MinPrice;
            this.FirstVariation = new ProductDetail_VariationDTO(Item.FirstVariation);

            this.SecondVariation = new ProductDetail_VariationDTO(Item.SecondVariation);

        }
    }

    public class ProductDetail_ItemFilterDTO : FilterDTO
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

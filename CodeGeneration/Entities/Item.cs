
using System;
using System.Collections.Generic;
using Common;

namespace WG.Entities
{
    public class Item : DataEntity
    {
        
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public long MinPrice { get; set; }
        public Variation FirstVariation { get; set; }
        public Product Product { get; set; }
        public Variation SecondVariation { get; set; }
        public List<DiscountContent> DiscountContents { get; set; }
        public List<OrderContent> OrderContents { get; set; }
        public List<Stock> Stocks { get; set; }
    }

    public class ItemFilter : FilterEntity
    {
        
        public LongFilter Id { get; set; }
        public LongFilter ProductId { get; set; }
        public LongFilter FirstVariationId { get; set; }
        public LongFilter SecondVariationId { get; set; }
        public StringFilter SKU { get; set; }
        public LongFilter Price { get; set; }
        public LongFilter MinPrice { get; set; }
        public List<long> Ids { get; set; }
        public List<long> ExceptIds { get; set; }

        public ItemOrder OrderBy {get; set;}
        public ItemSelect Selects {get; set;}
    }

    public enum ItemOrder
    {
        
        Id = 1,
        Product = 2,
        FirstVariation = 3,
        SecondVariation = 4,
        SKU = 5,
        Price = 6,
        MinPrice = 7,
    }
    
    [Flags]
    public enum ItemSelect:long
    {
        ALL = E.ALL,
        
        Id = E._1,
        Product = E._2,
        FirstVariation = E._3,
        SecondVariation = E._4,
        SKU = E._5,
        Price = E._6,
        MinPrice = E._7,
    }
}

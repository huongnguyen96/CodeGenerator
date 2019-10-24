using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDAO
    {
        public ItemDAO()
        {
            DiscountContents = new HashSet<DiscountContentDAO>();
            OrderContents = new HashSet<OrderContentDAO>();
            Stocks = new HashSet<StockDAO>();
        }

        public long Id { get; set; }
        public long ProductId { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }
        public long MinPrice { get; set; }

        public virtual VariationDAO FirstVariation { get; set; }
        public virtual ProductDAO Product { get; set; }
        public virtual VariationDAO SecondVariation { get; set; }
        public virtual ICollection<DiscountContentDAO> DiscountContents { get; set; }
        public virtual ICollection<OrderContentDAO> OrderContents { get; set; }
        public virtual ICollection<StockDAO> Stocks { get; set; }
    }
}

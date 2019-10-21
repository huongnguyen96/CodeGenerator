using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class UnitDAO
    {
        public UnitDAO()
        {
            DiscountItems = new HashSet<DiscountItemDAO>();
            Stocks = new HashSet<StockDAO>();
        }

        public long Id { get; set; }
        public long FirstVariationId { get; set; }
        public long? SecondVariationId { get; set; }
        public long? ThirdVariationId { get; set; }
        public string SKU { get; set; }
        public long Price { get; set; }

        public virtual VariationDAO FirstVariation { get; set; }
        public virtual VariationDAO SecondVariation { get; set; }
        public virtual VariationDAO ThirdVariation { get; set; }
        public virtual ICollection<DiscountItemDAO> DiscountItems { get; set; }
        public virtual ICollection<StockDAO> Stocks { get; set; }
    }
}

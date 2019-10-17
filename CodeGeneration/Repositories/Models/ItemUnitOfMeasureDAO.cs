using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemUnitOfMeasureDAO
    {
        public ItemUnitOfMeasureDAO()
        {
            ItemStocks = new HashSet<ItemStockDAO>();
            Items = new HashSet<ItemDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ItemStockDAO> ItemStocks { get; set; }
        public virtual ICollection<ItemDAO> Items { get; set; }
    }
}

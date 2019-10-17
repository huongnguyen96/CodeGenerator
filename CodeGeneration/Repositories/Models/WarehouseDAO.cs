using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class WarehouseDAO
    {
        public WarehouseDAO()
        {
            ItemStocks = new HashSet<ItemStockDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long SupplierId { get; set; }

        public virtual SupplierDAO Supplier { get; set; }
        public virtual ICollection<ItemStockDAO> ItemStocks { get; set; }
    }
}

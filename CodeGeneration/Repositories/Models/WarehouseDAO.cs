using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class WarehouseDAO
    {
        public WarehouseDAO()
        {
            Stocks = new HashSet<StockDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long PartnerId { get; set; }

        public virtual MerchantDAO Partner { get; set; }
        public virtual ICollection<StockDAO> Stocks { get; set; }
    }
}

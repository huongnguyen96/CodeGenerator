using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class StockDAO
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }

        public virtual ItemDAO Item { get; set; }
        public virtual WarehouseDAO Warehouse { get; set; }
    }
}

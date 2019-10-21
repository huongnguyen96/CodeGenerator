using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class StockDAO
    {
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long WarehouseId { get; set; }
        public long Quantity { get; set; }

        public virtual UnitDAO Unit { get; set; }
        public virtual WarehouseDAO Warehouse { get; set; }
    }
}

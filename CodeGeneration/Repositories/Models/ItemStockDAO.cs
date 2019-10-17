using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemStockDAO
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }

        public virtual ItemDAO Item { get; set; }
        public virtual ItemUnitOfMeasureDAO UnitOfMeasure { get; set; }
        public virtual WarehouseDAO Warehouse { get; set; }
    }
}

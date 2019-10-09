using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemMaterialDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid SourceItemId { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public decimal? Quantity { get; set; }
        public Guid BusinessGroupId { get; set; }
        public Guid ItemDetailId { get; set; }

        public virtual ItemDetailDAO ItemDetail { get; set; }
        public virtual ItemDAO SourceItem { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMeasure { get; set; }
    }
}

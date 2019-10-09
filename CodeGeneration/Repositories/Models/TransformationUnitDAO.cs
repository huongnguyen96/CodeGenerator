using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class TransformationUnitDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid ItemDetailId { get; set; }
        public Guid BaseUnitId { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PrimaryPrice { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual UnitOfMeasureDAO BaseUnit { get; set; }
        public virtual ItemDetailDAO ItemDetail { get; set; }
    }
}

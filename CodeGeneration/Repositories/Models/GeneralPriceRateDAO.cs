using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class GeneralPriceRateDAO
    {
        public Guid Id { get; set; }
        public Guid BusinessGroupId { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid ItemId { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public decimal? Price { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class ItemDiscountDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public Guid ItemDetailId { get; set; }
        public int QuantityFrom { get; set; }
        public int QuantityTo { get; set; }
        public decimal? Rate { get; set; }
        public string DiscountType { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual ItemDetailDAO ItemDetail { get; set; }
    }
}

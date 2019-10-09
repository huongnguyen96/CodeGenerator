using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class SupplierDetail_SupplierGroupingDAO
    {
        public Guid Id { get; set; }
        public Guid SupplierGroupingId { get; set; }
        public Guid SupplierDetailId { get; set; }
        public long CX { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual SupplierDetailDAO SupplierDetail { get; set; }
        public virtual SupplierGroupingDAO SupplierGrouping { get; set; }
    }
}

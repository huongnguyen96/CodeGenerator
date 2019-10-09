using System;
using System.Collections.Generic;

namespace CodeGeneration.Repositories.Models
{
    public partial class SupplierGroupingDAO
    {
        public SupplierGroupingDAO()
        {
            SupplierDetail_SupplierGroupings = new HashSet<SupplierDetail_SupplierGroupingDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Disabled { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LegalEntityId { get; set; }
        public Guid BusinessGroupId { get; set; }

        public virtual LegalEntityDAO LegalEntity { get; set; }
        public virtual ICollection<SupplierDetail_SupplierGroupingDAO> SupplierDetail_SupplierGroupings { get; set; }
    }
}
